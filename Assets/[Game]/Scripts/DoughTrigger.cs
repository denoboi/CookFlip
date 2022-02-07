using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;

public class DoughTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("DoughSpilled");
    }
}
