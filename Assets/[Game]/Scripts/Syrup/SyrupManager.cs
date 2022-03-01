using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupManager : MonoBehaviour
{
    [SerializeField] private GameObject _syrup;
    

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
        _syrup.SetActive(true);
    }
}
