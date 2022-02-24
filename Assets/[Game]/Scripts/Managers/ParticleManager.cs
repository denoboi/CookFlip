using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public static ParticleManager instance;

    public ParticleSystem SmokeParticle1, SmokeParticle2, SmokeParticle3, SmokeParticle4, BurntParticle, UnhappyParticle, HappyParticle;
    

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
        SmokeParticle2.Play();
        SmokeParticle3.Play();
        SmokeParticle4.Play();
    }

    public void BurntParticleMethod()
    {
        BurntParticle.Play();
    }

    public void UnhappyParticleMethod()
    {
        UnhappyParticle.Play();

    }
}
