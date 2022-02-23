using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using DG.Tweening;

public class HeatParticleColorController : MonoBehaviour
{
    
    
    public ParticleSystem _particleSystem;
    public ParticleSystem ParticleSystem => _particleSystem == null ? _particleSystem = GetComponentInChildren<ParticleSystem>() : _particleSystem;

    private string _tweenID;

    private void Awake()
    {
        

        _tweenID = GetInstanceID() + "ParticleID";

       
        
    }

    public Color DefaultColor;
    public Color CookingColor;

    private void OnTriggerEnter(Collider other)
    {
        DoughTrigger doughTrigger = other.GetComponent<DoughTrigger>();

        if(doughTrigger != null)
        {
            OnParticleEnter();
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        DoughTrigger doughTrigger = other.GetComponent<DoughTrigger>();

        if (doughTrigger != null)
        {
            OnParticleExit();
        }
    }


    public void OnParticleEnter()
    {
        float percentage = 0;

        DOTween.Kill(_tweenID);

        DOTween.To(() => percentage, (x) => percentage = x, 1, 0.1f).SetId(_tweenID).OnUpdate(() => {
            Color color = Color.Lerp(DefaultColor, CookingColor, percentage);
            var main = ParticleSystem.main;
            main.startColor = color;
        });

        


        Debug.Log("ColorChanged");
        
    }

    public void OnParticleExit()
    {

        float percentage = 0;

        DOTween.Kill(_tweenID);
        
        Debug.Log("ColorChanged2");

        
        DOTween.To(() => percentage, (x) => percentage = x, 1, 0.15f).SetId(_tweenID).OnUpdate(() => {
            Color color = Color.Lerp(CookingColor, DefaultColor, percentage);
            var main = ParticleSystem.main;
            main.startColor = color;
        });
    }
}



