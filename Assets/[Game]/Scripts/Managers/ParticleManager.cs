using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public static ParticleManager instance;

    public ParticleSystem SmokeParticle, BurntParticle, UnhappyParticle, HappyParticle;
    

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
        SmokeParticle.Play();
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
