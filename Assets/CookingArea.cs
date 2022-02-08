using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.SplineMovementSystem;

public class CookingArea : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    private bool isEntered = false;
    private bool isExited = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {




        if(!isEntered)
        {
            Debug.Log("Entered");
            isEntered = true;
            SplineCharacterMovementController controller = GetComponent<SplineCharacterMovementController>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!isExited)
        {
            isExited = true;
            
        }
    }
}
