using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;

public class DoughTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SplineCharacter splineCharacter = other.transform.root.GetComponentInChildren<SplineCharacter>();

        if(splineCharacter !=null)
        {
            Debug.Log("DoughSpilled");
        }
    }
}
