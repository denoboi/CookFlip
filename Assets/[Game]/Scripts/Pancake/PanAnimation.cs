using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;

public class PanAnimation : MonoBehaviour
{
    private Animator anim;
    public Animator animator;

    PancakeStats _stats;



    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        PanFlipAnimation();
    }

    void PanFlipAnimation()
    {
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetTrigger("PanFlip");


            if (PancakeStats.Instance.currentFace == 0)
            {
                animator.SetTrigger("BackFace");
                
            }

            else
            {
                animator.SetTrigger("FrontFace");
                
            }

            PancakeStats.Instance.ChangeFace();
                

            
            


        }
        
    }

}
