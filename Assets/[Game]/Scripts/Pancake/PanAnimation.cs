using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;
using DG.Tweening;

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
        DOTween.Init();
    }


    private void Update()
    {


        PanFlipAnimation();


        PanThrowLeftAnimation();

       
        
        

    }

    void PanFlipAnimation()
    {
        if (GameManager.Instance.IsStageCompleted) return;

        if (Input.GetMouseButtonUp(0) && PancakeStats.Instance.isCooked == false && PancakeStats.Instance.isBurnt == false && doughTriggerScript.isDoughAvailable)
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
        if (StackManager.instance.CurrentPanCake == null)
            return;
        


        if (Input.GetMouseButtonUp(0) && PancakeStats.Instance.isCooked == true || Input.GetMouseButtonUp(0) && PancakeStats.Instance.isBurnt == true)
        {
            anim.SetTrigger("IfCooked");
    
        }

    }


}
