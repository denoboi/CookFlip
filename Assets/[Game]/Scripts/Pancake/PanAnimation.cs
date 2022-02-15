using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;

public class PanAnimation : MonoBehaviour
{
    private Animator anim;
    public Animator animator;



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
            Debug.Log("flip");
            anim.SetBool("PanFlip", true);
            animator.SetBool("CakeFlip", true);


        }
        else if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("PanFlip", false);
            animator.SetBool("CakeFlip", false);
        }
    }

}
