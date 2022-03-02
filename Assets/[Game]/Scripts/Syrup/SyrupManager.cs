using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupManager : MonoBehaviour
{
    [SerializeField] private GameObject _syrup;
    public bool isOnPlate;
    public bool isAlreadyTaken;
    

    //public Pancake ovenTrigger => OvenTrigger == null ? OvenTrigger = GetComponentInChildren<OvenTrigger>() : OvenTrigger;

    private void OnTriggerEnter(Collider other)
    {


        if(other.CompareTag("SyrupBone"))
        {
           
            Debug.Log("SyrupAdded");
            EnableSyrup();

        }
    }

    public void EnableSyrup()
    {
        if(!isAlreadyTaken && isOnPlate)
        {
            _syrup.SetActive(true);
            _syrup.transform.Rotate(Vector3.up * Random.Range(45, 90));
            isAlreadyTaken = true;
           
        }
       
    }
}
