using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

public class HeatParticleColorController : MonoBehaviour
{
    public static HeatParticleColorController instance;

    private ParticleSystem _particleSystem;
    private ParticleSystem ParticleSystem => _particleSystem == null ? _particleSystem = GetComponent<ParticleSystem>() : _particleSystem;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

       
        
    }

    public Color DefaultColor;
    public Color CookingColor;


    public void OnParticleEnter()
    {
        Color color = Color.Lerp(DefaultColor, CookingColor, 0.1f);
        Debug.Log("ColorChanged");
        
    }

    public void OnParticleExit()
    {
        Color color = Color.Lerp(CookingColor, DefaultColor, 0.1f);
        Debug.Log("ColorChanged2");
    }
}



