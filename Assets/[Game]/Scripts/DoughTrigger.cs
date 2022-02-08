using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;

public class DoughTrigger : MonoBehaviour
{

    [SerializeField] private GameObject dough;

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


}
