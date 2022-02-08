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
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PancakeStats stats = other.transform.root.GetComponentInChildren<PancakeStats>();


        controller = stats.GetComponent<SplineCharacterMovementController>();

        if (!isEntered)
        {
            Debug.Log("Entered");
            isEntered = true;
            controller.SetSpeed(controller.MovementData.CookingSpeed);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        controller = stats.GetComponent<SplineCharacterMovementController>();


        if (!isExited)
        {
            Debug.Log("Exited");
            isExited = true;
            controller.SetSpeed(controller.MovementData.DefaultSpeed);
            
        }
    }
}
