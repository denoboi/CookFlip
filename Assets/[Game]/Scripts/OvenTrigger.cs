using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cooked");

    }
}
