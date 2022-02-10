using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoughTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dough;
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("DoughSpilled");
        if (other.CompareTag("PancakeDoughBone"))
            EnableDough();
    }


    public void EnableDough()
    {
        dough.SetActive(true);
    }
}
