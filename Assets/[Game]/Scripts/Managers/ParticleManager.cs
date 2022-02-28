using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public static ParticleManager instance;

    public ParticleSystem SmokeParticle1, BurntParticle, UnhappyParticle, HappyParticle;
    

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
