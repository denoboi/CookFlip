using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public static ParticleManager instance;

    public ParticleSystem SmokeParticle1, SmokeParticle2, SmokeParticle3, SmokeParticle4, SmokeParticle5, SmokeParticle6, SmokeParticle7, SmokeParticle8, SmokeParticle9, BurntParticle, UnhappyParticle, HappyParticle;
    

    private void Awake()
    {
        instance = this;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SmokeParticleMethod()
    {
        SmokeParticle1.Play();
        //SmokeParticle2.Play();
        //SmokeParticle3.Play();
        //SmokeParticle4.Play();
        //SmokeParticle5.Play();
        //SmokeParticle6.Play();
        //SmokeParticle7.Play();
        //SmokeParticle8.Play();
        //SmokeParticle9.Play();
    }

    public void BurntParticleMethod()
    {
        BurntParticle.Play();
    }

    public void UnhappyParticleMethod()
    {
        UnhappyParticle.Play();

    }

    public void SmokeParticleMethodStop()
    {
        SmokeParticle1.Stop();
    }

    public void BurntParticleMethodStop()
    {
        BurntParticle.Stop();
    }

    public void HappyParticleMethod()
    {
        HappyParticle.Play();
    }
}
