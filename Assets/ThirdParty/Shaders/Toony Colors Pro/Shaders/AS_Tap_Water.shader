// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "AS_Tap_Water"
{
	Properties
	{
		_SpecColor("Specular Color",Color)=(1,1,1,1)
		_Water_Fallof("Water_Fallof", Range( 0 , 41.54)) = 5
		_Water_Color_A("Water_Color_A", Color) = (0.3788679,0.6878227,0.7830189,0)
		_Metalic("Metalic", Range( 0 , 1)) = 1
		_Smoothness("Smoothness", Range( 0 , 10)) = 0.04
		_Cut_Height("Cut_Height", Range( 0 , 1)) = 0.35
		_Rim_Power("Rim_Power", Range( 0 , 5)) = 0
		_Rim_Scale("Rim_Scale", Range( 0 , 10)) = 0
		_Water_Color_b("Water_Color_b", Color) = (0,0.7868266,1,0)
		_Rim_Color("Rim_Color", Color) = (0.4838554,0.7455673,1,1)
		_Noise_Texture("Noise_Texture", 2D) = "bump" {}
		_Normal_Map("Normal_Map", 2D) = "bump" {}
		_Water_Noise("Water_Noise", Range( 0 , 1)) = 0.18
		_Direction("Direction", Vector) = (0,-1,0,0)
		_Water_Opacity("Water_Opacity", Range( 0 , 10)) = 0.8
		_Water_Speed("Water_Speed", Range( 0 , 2)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf BlinnPhong alpha:fade keepalpha noshadow exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			INTERNAL_DATA
			float3 worldNormal;
		};

		uniform float _Water_Noise;
		uniform sampler2D _Noise_Texture;
		uniform float _Water_Speed;
		uniform float2 _Direction;
		uniform sampler2D _Normal_Map;
		uniform float4 _Water_Color_A;
		uniform float4 _Water_Color_b;
		uniform float _Rim_Scale;
		uniform float _Rim_Power;
		uniform float4 _Rim_Color;
		uniform float _Metalic;
		uniform float _Smoothness;
		uniform float _Cut_Height;
		uniform float _Water_Fallof;
		uniform float _Water_Opacity;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float mulTime136 = _Time.y * _Water_Speed;
			float2 panner132 = ( mulTime136 * _Direction + v.texcoord.xy);
			float3 tex2DNode129 = UnpackNormal( tex2Dlod( _Noise_Texture, float4( panner132, 0, 0.0) ) );
			float3 ase_vertexNormal = v.normal.xyz;
			float3 Vertex_Animation143 = ( saturate( ( _Water_Noise * ( tex2DNode129.b + tex2DNode129.g ) ) ) * ase_vertexNormal );
			v.vertex.xyz += Vertex_Animation143;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float mulTime136 = _Time.y * _Water_Speed;
			float2 panner132 = ( mulTime136 * _Direction + i.uv_texcoord);
			float3 tex2DNode156 = UnpackNormal( tex2D( _Normal_Map, panner132 ) );
			float3 Normal97 = tex2DNode156;
			o.Normal = Normal97;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_worldTangent = WorldNormalVector( i, float3( 1, 0, 0 ) );
			float3 ase_worldBitangent = WorldNormalVector( i, float3( 0, 1, 0 ) );
			float3x3 ase_tangentToWorldFast = float3x3(ase_worldTangent.x,ase_worldBitangent.x,ase_worldNormal.x,ase_worldTangent.y,ase_worldBitangent.y,ase_worldNormal.y,ase_worldTangent.z,ase_worldBitangent.z,ase_worldNormal.z);
			float fresnelNdotV92 = dot( mul(ase_tangentToWorldFast,tex2DNode156), ase_worldViewDir );
			float fresnelNode92 = ( 0.1 + _Rim_Scale * pow( max( 1.0 - fresnelNdotV92 , 0.0001 ), _Rim_Power ) );
			float4 lerpResult164 = lerp( _Water_Color_A , _Water_Color_b , fresnelNode92);
			float4 albedo184 = lerpResult164;
			o.Albedo = albedo184.rgb;
			float temp_output_131_0 = saturate( fresnelNode92 );
			float4 Emissive98 = ( ( temp_output_131_0 * _Rim_Color ) * _Rim_Color.a );
			o.Emission = Emissive98.rgb;
			o.Specular = _Metalic;
			o.Gloss = _Smoothness;
			float water_Depth166 = temp_output_131_0;
			float lerpResult187 = lerp( 0.4 , 0.2 , water_Depth166);
			float Opacity_Mask100 = ( saturate( ( ( ( 1.0 - (-0.2 + (_Cut_Height - 0.0) * (1.2 - -0.2) / (1.0 - 0.0)) ) - i.uv_texcoord.y ) * _Water_Fallof ) ) * saturate( lerpResult187 ) );
			o.Alpha = ( Opacity_Mask100 * _Water_Opacity );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18921
611.6289;832.33;1526;511.9175;-3060.796;1687.339;1.280539;True;False
Node;AmplifyShaderEditor.RangedFloatNode;193;533.5057,-2540.096;Inherit;False;Property;_Water_Speed;Water_Speed;15;0;Create;True;0;0;0;False;0;False;0;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;136;848.0671,-2512.211;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;134;820.2688,-2776.035;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;133;856.2688,-2641.205;Inherit;False;Property;_Direction;Direction;13;0;Create;True;0;0;0;False;0;False;0,-1;0,-1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;132;1121.627,-2670.77;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;156;1347.737,-2756.858;Inherit;True;Property;_Normal_Map;Normal_Map;11;0;Create;True;0;0;0;False;0;False;-1;None;f1fa4639f979a244db79208ca66f899d;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;126;1740.851,-1867.461;Inherit;False;Property;_Rim_Scale;Rim_Scale;7;0;Create;True;0;0;0;False;0;False;0;1.68;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;123;1747.363,-1750.553;Inherit;False;Property;_Rim_Power;Rim_Power;6;0;Create;True;0;0;0;False;0;False;0;3;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;122;-1797.997,-1464.458;Inherit;False;2064.067;641.2892;water_with;13;102;107;121;110;108;111;112;117;100;167;171;187;189;;0,0.894588,1,1;0;0
Node;AmplifyShaderEditor.FresnelNode;92;2190.227,-2086.233;Inherit;True;Standard;TangentNormal;ViewDir;True;True;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0.1;False;2;FLOAT;5.53;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;102;-1747.997,-1414.458;Inherit;False;Property;_Cut_Height;Cut_Height;5;0;Create;True;0;0;0;False;0;False;0.35;0.527;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;121;-1427.631,-1376.036;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.2;False;4;FLOAT;1.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;131;2501.23,-1830.314;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;107;-1154.431,-1397.955;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;108;-1434.643,-1135.285;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;166;2787.521,-1804.451;Inherit;False;water_Depth;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;111;-1125.961,-938.9728;Inherit;False;Property;_Water_Fallof;Water_Fallof;1;0;Create;True;0;0;0;False;0;False;5;6;0;41.54;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;110;-914.8293,-1360.541;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;167;-776.4723,-1079.185;Inherit;True;166;water_Depth;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;129;1351.178,-3005.133;Inherit;True;Property;_Noise_Texture;Noise_Texture;10;0;Create;True;0;0;0;False;0;False;-1;None;f1fa4639f979a244db79208ca66f899d;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;148;2026.279,-2723.076;Inherit;False;1656.678;494.7827;Water_noise;5;143;146;145;147;141;;0,0.8244033,1,0.4980392;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;112;-620.697,-1370.804;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;194;2032.096,-2845.744;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;142;2122.635,-2985.581;Inherit;False;Property;_Water_Noise;Water_Noise;12;0;Create;True;0;0;0;False;0;False;0.18;0.02;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;187;-448.7537,-1106.21;Inherit;False;3;0;FLOAT;0.4;False;1;FLOAT;0.2;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;141;2332.518,-2666.927;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;117;-272.492,-1360.56;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;94;2314.084,-1525.259;Inherit;False;Property;_Rim_Color;Rim_Color;9;0;Create;True;0;0;0;False;0;False;0.4838554,0.7455673,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;189;-256.7537,-1098.21;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;147;2486.959,-2456.166;Inherit;True;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;171;-31.47229,-1221.185;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;93;2742.454,-1640.913;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;4;2744.501,-2214.597;Inherit;False;Property;_Water_Color_A;Water_Color_A;2;0;Create;True;0;0;0;False;0;False;0.3788679,0.6878227,0.7830189,0;0.2014108,0.5313714,0.6226415,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;145;2576.376,-2673.076;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;181;2904.007,-2016.906;Inherit;False;Property;_Water_Color_b;Water_Color_b;8;0;Create;True;0;0;0;False;0;False;0,0.7868266,1,0;0.2777028,0.5699882,0.8584906,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;95;2990.068,-1529.211;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;164;3688.401,-2133.55;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;146;2886.881,-2669.223;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;100;93.02475,-1334.692;Inherit;False;Opacity_Mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;184;3971.163,-2073.321;Inherit;False;albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;98;3258.294,-1560.426;Inherit;True;Emissive;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;143;3174.581,-2651.688;Inherit;False;Vertex_Animation;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;101;3735.574,-1095.498;Inherit;True;100;Opacity_Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;97;1783.85,-2691.794;Inherit;False;Normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;163;3702.663,-877.0109;Inherit;False;Property;_Water_Opacity;Water_Opacity;14;0;Create;True;0;0;0;False;0;False;0.8;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;96;3694.212,-1464.216;Inherit;False;97;Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;6;3675.836,-1245.637;Inherit;False;Property;_Metalic;Metalic;3;0;Create;True;0;0;0;False;0;False;1;0.625;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;99;3701.018,-1378.493;Inherit;False;98;Emissive;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;144;3831.239,-750.1811;Inherit;False;143;Vertex_Animation;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;183;4184.828,-1661.004;Inherit;False;184;albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;162;4143.622,-1277.028;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0.18;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;3815.277,-1333.395;Inherit;False;Property;_Smoothness;Smoothness;4;0;Create;True;0;0;0;False;0;False;0.04;0.255;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;4529.184,-1507.098;Float;False;True;-1;2;ASEMaterialInspector;0;0;BlinnPhong;AS_Tap_Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0;True;False;0;False;Transparent;;Transparent;ForwardOnly;18;all;True;True;True;False;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;1;False;-1;10;False;-1;0;False;-1;0;False;-1;1;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;11;-1;0;False;0;0;False;-1;0;0;False;-1;0;0;0;False;0.3;False;-1;0;False;24;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;136;0;193;0
WireConnection;132;0;134;0
WireConnection;132;2;133;0
WireConnection;132;1;136;0
WireConnection;156;1;132;0
WireConnection;92;0;156;0
WireConnection;92;2;126;0
WireConnection;92;3;123;0
WireConnection;121;0;102;0
WireConnection;131;0;92;0
WireConnection;107;0;121;0
WireConnection;166;0;131;0
WireConnection;110;0;107;0
WireConnection;110;1;108;2
WireConnection;129;1;132;0
WireConnection;112;0;110;0
WireConnection;112;1;111;0
WireConnection;194;0;129;3
WireConnection;194;1;129;2
WireConnection;187;2;167;0
WireConnection;141;0;142;0
WireConnection;141;1;194;0
WireConnection;117;0;112;0
WireConnection;189;0;187;0
WireConnection;171;0;117;0
WireConnection;171;1;189;0
WireConnection;93;0;131;0
WireConnection;93;1;94;0
WireConnection;145;0;141;0
WireConnection;95;0;93;0
WireConnection;95;1;94;4
WireConnection;164;0;4;0
WireConnection;164;1;181;0
WireConnection;164;2;92;0
WireConnection;146;0;145;0
WireConnection;146;1;147;0
WireConnection;100;0;171;0
WireConnection;184;0;164;0
WireConnection;98;0;95;0
WireConnection;143;0;146;0
WireConnection;97;0;156;0
WireConnection;162;0;101;0
WireConnection;162;1;163;0
WireConnection;0;0;183;0
WireConnection;0;1;96;0
WireConnection;0;2;99;0
WireConnection;0;3;6;0
WireConnection;0;4;5;0
WireConnection;0;9;162;0
WireConnection;0;11;144;0
ASEEND*/
//CHKSM=920B91421D3E148A6A0875967C2D8F215EABD0A2