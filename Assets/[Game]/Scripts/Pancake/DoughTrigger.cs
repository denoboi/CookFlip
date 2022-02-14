using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoughTrigger : MonoBehaviour
{
    //[SerializeField] private GameObject pancakeDough;
    [SerializeField] private GameObject dough;
    [SerializeField] private float delay;


    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("DoughSpilled");
        if (other.CompareTag("PancakeDoughBone"))
            EnableDough();
        
        
        DisableDough();
       
    }

    


    public void EnableDough()
    {
        dough.SetActive(true);
    }

    public void DisableDough()
    {
        StartCoroutine(DisableCo(dough));
    }

    private IEnumerator DisableCo(GameObject dough)
    {
        if(dough.activeSelf)
        {
            yield return new WaitForSeconds(delay);
            dough.SetActive(false);
        }
    }
}
