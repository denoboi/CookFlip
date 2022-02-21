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

    public Animator panThrowLeftAnimation;



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
        if (Input.GetMouseButtonUp(0) && PancakeStats.Instance.isCooked == false)
        {
            anim.SetTrigger("PanFlip");
            PancakeFlipAnimation();

        }

        
        
    }

    void PancakeFlipAnimation()
    {
        if (PancakeStats.Instance.currentFace == 0)
        {
            animator.SetTrigger("FrontFace");     
        }

        else
        {
            animator.SetTrigger("BackFace");
        }


        PancakeStats.Instance.ChangeFace();
    }


    void

}
