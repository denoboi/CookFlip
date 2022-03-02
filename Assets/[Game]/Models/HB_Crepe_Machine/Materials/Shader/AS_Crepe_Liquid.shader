// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/AS_Crepe_Liquid"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Direction("Direction", Vector) = (0,1,0,0)
		_Crepe_Color_A("Crepe_Color_A", Color) = (0.7921569,0.7254902,0.6588235,1)
		_Crepe_Color_B("Crepe_Color_B", Color) = (0.7176471,0.6313726,0.5490196,1)
		_Rim_Power("Rim_Power", Float) = 0.3
		_Rim_Scale("Rim_Scale", Float) = 1
		_Rim_Color("Rim_Color", Color) = (1,0.8784314,0.7098039,1)
		_Cut_Height("Cut_Height", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0

		struct appdata_full_custom
		{
			float4 vertex : POSITION;
			float4 tangent : TANGENT;
			float3 normal : NORMAL;
			float4 texcoord : TEXCOORD0;
			float4 texcoord1 : TEXCOORD1;
			float4 texcoord2 : TEXCOORD2;
			float4 texcoord3 : TEXCOORD3;
			float4 color : COLOR;
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform float2 _Direction;
		uniform float4 _Crepe_Color_A;
		uniform float4 _Crepe_Color_B;
		uniform float4 _Rim_Color;
		uniform float _Rim_Scale;
		uniform float _Rim_Power;
		uniform float _Cut_Height;
		uniform float _Cutoff = 0.5;


		float3 mod3D289( float3 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 mod3D289( float4 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 permute( float4 x ) { return mod3D289( ( x * 34.0 + 1.0 ) * x ); }

		float4 taylorInvSqrt( float4 r ) { return 1.79284291400159 - r * 0.85373472095314; }

		float snoise( float3 v )
		{
			const float2 C = float2( 1.0 / 6.0, 1.0 / 3.0 );
			float3 i = floor( v + dot( v, C.yyy ) );
			float3 x0 = v - i + dot( i, C.xxx );
			float3 g = step( x0.yzx, x0.xyz );
			float3 l = 1.0 - g;
			float3 i1 = min( g.xyz, l.zxy );
			float3 i2 = max( g.xyz, l.zxy );
			float3 x1 = x0 - i1 + C.xxx;
			float3 x2 = x0 - i2 + C.yyy;
			float3 x3 = x0 - 0.5;
			i = mod3D289( i);
			float4 p = permute( permute( permute( i.z + float4( 0.0, i1.z, i2.z, 1.0 ) ) + i.y + float4( 0.0, i1.y, i2.y, 1.0 ) ) + i.x + float4( 0.0, i1.x, i2.x, 1.0 ) );
			float4 j = p - 49.0 * floor( p / 49.0 );  // mod(p,7*7)
			float4 x_ = floor( j / 7.0 );
			float4 y_ = floor( j - 7.0 * x_ );  // mod(j,N)
			float4 x = ( x_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 y = ( y_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 h = 1.0 - abs( x ) - abs( y );
			float4 b0 = float4( x.xy, y.xy );
			float4 b1 = float4( x.zw, y.zw );
			float4 s0 = floor( b0 ) * 2.0 + 1.0;
			float4 s1 = floor( b1 ) * 2.0 + 1.0;
			float4 sh = -step( h, 0.0 );
			float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
			float4 a1 = b1.xzyw + s1.xzyw * sh.zzww;
			float3 g0 = float3( a0.xy, h.x );
			float3 g1 = float3( a0.zw, h.y );
			float3 g2 = float3( a1.xy, h.z );
			float3 g3 = float3( a1.zw, h.w );
			float4 norm = taylorInvSqrt( float4( dot( g0, g0 ), dot( g1, g1 ), dot( g2, g2 ), dot( g3, g3 ) ) );
			g0 *= norm.x;
			g1 *= norm.y;
			g2 *= norm.z;
			g3 *= norm.w;
			float4 m = max( 0.6 - float4( dot( x0, x0 ), dot( x1, x1 ), dot( x2, x2 ), dot( x3, x3 ) ), 0.0 );
			m = m* m;
			m = m* m;
			float4 px = float4( dot( x0, g0 ), dot( x1, g1 ), dot( x2, g2 ), dot( x3, g3 ) );
			return 42.0 * dot( m, px);
		}


		float2 voronoihash92( float2 p )
		{
			p = p - 10 * floor( p / 10 );
			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
			return frac( sin( p ) *43758.5453);
		}


		float voronoi92( float2 v, float time, inout float2 id, inout float2 mr, float smoothness, inout float2 smoothId )
		{
			float2 n = floor( v );
			float2 f = frac( v );
			float F1 = 8.0;
			float F2 = 8.0; float2 mg = 0;
			for ( int j = -1; j <= 1; j++ )
			{
				for ( int i = -1; i <= 1; i++ )
			 	{
			 		float2 g = float2( i, j );
			 		float2 o = voronoihash92( n + g );
					o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
					float d = 0.5 * dot( r, r );
			 		if( d<F1 ) {
			 			F2 = F1;
			 			F1 = d; mg = g; mr = r; id = o;
			 		} else if( d<F2 ) {
			 			F2 = d;
			
			 		}
			 	}
			}
			return (F2 + F1) * 0.5;
		}


		void vertexDataFunc( inout appdata_full_custom v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float2 uv_TexCoord25 = v.texcoord.xy * float2( 1,1 );
			float2 panner27 = ( _Time.y * _Direction + uv_TexCoord25);
			float2 Panner38 = panner27;
			float simplePerlin3D70 = snoise( float3( Panner38 ,  0.0 )*2.0 );
			float lerpResult48 = lerp( 0.0 , simplePerlin3D70 , saturate( ( ( 1.0 - v.texcoord.xy.y ) * 3.0 ) ));
			float temp_output_64_0 = ( 1.0 - v.texcoord.xy.y );
			v.vertex.xyz += ( ase_vertexNormal * ( ( 0.05 * lerpResult48 ) + ( (-0.05 + (temp_output_64_0 - 0.1) * (-0.01 - -0.05) / (2.0 - 0.1)) * temp_output_64_0 * 2.0 ) ) );
			v.vertex.w = 1;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float time92 = ( _Time.y * 2.0 );
			float2 voronoiSmoothId92 = 0;
			float2 uv_TexCoord25 = i.uv_texcoord * float2( 1,1 );
			float2 panner27 = ( _Time.y * _Direction + uv_TexCoord25);
			float2 Panner38 = panner27;
			float2 coords92 = ( Panner38 * float2( 1,0.1 ) ) * 11.0;
			float2 id92 = 0;
			float2 uv92 = 0;
			float voroi92 = voronoi92( coords92, time92, id92, uv92, 0, voronoiSmoothId92 );
			float temp_output_112_0 = (0.0 + (voroi92 - 0.0) * (3.0 - 0.0) / (1.0 - 0.0));
			float4 lerpResult77 = lerp( _Crepe_Color_A , _Crepe_Color_B , temp_output_112_0);
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV79 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode79 = ( 0.2 + _Rim_Scale * pow( 1.0 - fresnelNdotV79, _Rim_Power ) );
			o.Emission = ( lerpResult77 + ( _Rim_Color * fresnelNode79 ) ).rgb;
			o.Alpha = 1;
			clip( step( _Cut_Height , i.uv_texcoord.y ) - _Cutoff );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit keepalpha fullforwardshadows noshadow vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full_custom v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18921
-2533.608;0;2533;1402;1483.866;343.4687;1;True;True
Node;AmplifyShaderEditor.CommentaryNode;40;-2197.31,-731.9224;Inherit;False;833.8044;424.6281;Comment;6;24;25;26;27;38;45;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;45;-2194.051,-686.8823;Inherit;False;Constant;_Texture_Scale;Texture_Scale;2;0;Create;True;0;0;0;False;0;False;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;24;-2119.511,-418.0986;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;25;-2015.41,-693.0223;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,12;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;26;-2111.31,-547.0927;Inherit;False;Property;_Direction;Direction;1;0;Create;True;0;0;0;False;0;False;0,1;0,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;27;-1844.16,-590.6431;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;37;-2244.451,773.9176;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;38;-1586.351,-573.5823;Inherit;False;Panner;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;47;-1922.772,740.1255;Inherit;True;2;0;FLOAT;1;False;1;FLOAT;0.58;False;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;107;-1458.434,76.03766;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;39;-2005.159,524.3384;Inherit;False;38;Panner;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;-1653.596,759.3019;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;87;-1560.963,-207.065;Inherit;False;38;Panner;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;53;-1056.365,945.0848;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;49;-1370.42,764.2435;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;64;-786.6823,948.3254;Inherit;True;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;108;-1214.434,91.03766;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;93;-1360.985,-87.47137;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;1,0.1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;70;-1618.595,467.2368;Inherit;True;Simplex3D;False;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;68;-528.6823,1109.325;Inherit;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;82;-472.6433,63.40265;Inherit;False;Property;_Rim_Scale;Rim_Scale;5;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;92;-1058.986,-106.4714;Inherit;True;0;0;1;3;1;True;10;False;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;7.75;False;2;FLOAT;11;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.LerpOp;48;-917.2186,443.8209;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;65;-520.6823,782.3254;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0.1;False;2;FLOAT;2;False;3;FLOAT;-0.05;False;4;FLOAT;-0.01;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;83;-486.6433,156.4026;Inherit;False;Property;_Rim_Power;Rim_Power;4;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;29;-374.7275,-603.9558;Inherit;False;Property;_Crepe_Color_A;Crepe_Color_A;2;0;Create;True;0;0;0;False;0;False;0.7921569,0.7254902,0.6588235,1;0.7921569,0.7254902,0.6588235,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;85;-276.4321,-119.5974;Inherit;False;Property;_Rim_Color;Rim_Color;6;0;Create;True;0;0;0;False;0;False;1,0.8784314,0.7098039,1;1,0.8784314,0.7098039,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;67;-329.6823,970.3254;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;-391.8183,493.4798;Inherit;False;2;2;0;FLOAT;0.05;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;78;-362.7723,-398.0687;Inherit;False;Property;_Crepe_Color_B;Crepe_Color_B;3;0;Create;True;0;0;0;False;0;False;0.7176471,0.6313726,0.5490196,1;0.7176471,0.6313726,0.5490196,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;112;-829.4336,-162.9623;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;79;-253.6433,64.40265;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0.2;False;2;FLOAT;0.3;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;36.35669,42.40265;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.NormalVertexDataNode;41;-400.4507,281.9175;Inherit;True;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;94;634.8179,61.52635;Inherit;True;Property;_Cut_Height;Cut_Height;7;0;Create;True;0;0;0;False;0;False;0;0.527;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;73;-108.6407,645.1995;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;97;612.4526,325.2262;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;77;73.04762,-231.2242;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;80;324.3567,-34.59735;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;101;1072.683,104.2222;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;111;-527.4336,-205.9623;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;62.30514,381.8549;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1760.488,-0.718428;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Custom/AS_Crepe_Liquid;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;25;0;45;0
WireConnection;27;0;25;0
WireConnection;27;2;26;0
WireConnection;27;1;24;0
WireConnection;38;0;27;0
WireConnection;47;1;37;2
WireConnection;50;0;47;0
WireConnection;49;0;50;0
WireConnection;64;1;53;2
WireConnection;108;0;107;2
WireConnection;93;0;87;0
WireConnection;70;0;39;0
WireConnection;92;0;93;0
WireConnection;92;1;108;0
WireConnection;48;1;70;0
WireConnection;48;2;49;0
WireConnection;65;0;64;0
WireConnection;67;0;65;0
WireConnection;67;1;64;0
WireConnection;67;2;68;0
WireConnection;74;1;48;0
WireConnection;112;0;92;0
WireConnection;79;2;82;0
WireConnection;79;3;83;0
WireConnection;84;0;85;0
WireConnection;84;1;79;0
WireConnection;73;0;74;0
WireConnection;73;1;67;0
WireConnection;77;0;29;0
WireConnection;77;1;78;0
WireConnection;77;2;112;0
WireConnection;80;0;77;0
WireConnection;80;1;84;0
WireConnection;101;0;94;0
WireConnection;101;1;97;2
WireConnection;111;0;112;0
WireConnection;35;0;41;0
WireConnection;35;1;73;0
WireConnection;0;2;80;0
WireConnection;0;10;101;0
WireConnection;0;11;35;0
ASEEND*/
//CHKSM=A76C60631BCCB035415749C6ADE728C6C76E02C2