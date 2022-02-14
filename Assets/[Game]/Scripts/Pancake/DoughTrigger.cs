using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoughTrigger : MonoBehaviour
{
    [SerializeField] private GameObject pancakeDough;
    [SerializeField] private GameObject dough;
    [SerializeField] private float delay;


    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("DoughSpilled");
        if (other.CompareTag("PancakeDoughBone"))
        {
            //basic dough
            EnableDough();

            //flippable dough
            EnablePancakeDough();
        }
            
        
        
        DisableDough();

        
       
    }
    

    //flippable dough
    public void EnablePancakeDough()
    {
        StartCoroutine(EnablePancakeCo(pancakeDough));
    }

    //basic dough
    public void EnableDough()
    {
        dough.SetActive(true);
    }

    //basic dough
    public void DisableDough()
    {
        StartCoroutine(DisableCo(dough));
    }


    //basic dough
    private IEnumerator DisableCo(GameObject dough)
    {
        if(dough.activeSelf)
        {
            yield return new WaitForSeconds(delay);
            dough.SetActive(false);
        }
    }


    //flippable dough
    private IEnumerator EnablePancakeCo(GameObject pancakeDough)
    {
        yield return new WaitForSeconds(delay);
        pancakeDough.SetActive(true);
    }
}
