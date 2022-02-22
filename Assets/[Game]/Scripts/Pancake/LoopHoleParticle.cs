using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopHoleParticle : MonoBehaviour
{
    public ParticleSystem splashParticle;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PancakeDoughBone"))
        {
            splashParticle.Play();
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.CompareTag("Pan"))
    //    {
    //        pancake.SetActive(true);
    //    }
    //}

}
