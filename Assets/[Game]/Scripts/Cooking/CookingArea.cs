using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.SplineMovementSystem;
using HCB.Core;

public class CookingArea : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    private bool isEntered = false;
    private bool isExited = false;
    SplineCharacterMovementController controller;
    PancakeStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = PancakeStats.Instance;
        controller = stats.GetComponent<SplineCharacterMovementController>();

    }

 

    private void OnTriggerEnter(Collider other)
    {
        PancakeStats stats = other.transform.root.GetComponentInChildren<PancakeStats>();
        controller = stats.GetComponent<SplineCharacterMovementController>();



        if (!isEntered)
        {
            Debug.Log("Entered");
            isEntered = true;
            

        }
    }

    private void OnTriggerExit(Collider other)
    {

        PancakeStats stats = other.transform.root.GetComponentInChildren<PancakeStats>();
        



        if (!isExited)
        {
            Debug.Log("Exited");
            isExited = true;
            
            
            
        }
    }
}
