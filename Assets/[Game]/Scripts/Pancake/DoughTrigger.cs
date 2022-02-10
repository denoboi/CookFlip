using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;

public class DoughTrigger : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private GameObject dough;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("DoughSpilled");
        if(other.CompareTag("PancakeDoughBone"))
            EnableDough();
    }


    public void EnableDough()
    {
        dough.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("flip");
            anim.SetBool("PanFlip", true);
            

        }
        else
        {
            anim.SetBool("PanFlip", false);
        }
    }


}
