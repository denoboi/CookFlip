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

    private Animator panThrowLeftAnimation;

    public DoughTrigger doughTriggerScript;



    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        PanFlipAnimation();
        PanThrowLeftAnimation();
        PlateAnimation();
        
        

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


    void PanThrowLeftAnimation()
    {
        if (Input.GetMouseButtonUp(0) && PancakeStats.Instance.isCooked == true || PancakeStats.Instance.isBurnt == true)
        {
            anim.SetTrigger("IfCooked");
            
        }

    }

    public void PlateAnimation()
    {
        if (Input.GetMouseButtonUp(0) && PancakeStats.Instance.isCooked == true || PancakeStats.Instance.isBurnt == true)
        {
            animator.SetTrigger("PancakeLeftAnim");
     
        }

        //PancakeStats.Instance.isInitialStates = true;



    }

}
