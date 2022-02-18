using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoughTrigger : MonoBehaviour
{
    [SerializeField] private GameObject creationPoint;
    [SerializeField] private GameObject dough;
    [SerializeField] private float delay;
    [SerializeField] private GameObject panCakeDoughPrefab;
    [SerializeField] private PanAnimation panAnimation;
    [SerializeField] private Transform plate;
    private GameObject pancake;


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
        StartCoroutine(EnablePancakeCo(creationPoint));
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
        pancake = Instantiate(panCakeDoughPrefab, creationPoint.transform.position, creationPoint.transform.rotation, creationPoint.transform.parent);
        panAnimation.animator = pancake.GetComponent<Animator>();

        
        
        
    }

    private void Update()
    {
        //Invoked when a button is clicked.
        if (Input.GetMouseButtonUp(0) && PancakeStats.Instance.isCooked == true)
        {
            pancake.transform.parent = plate.transform;
            Debug.Log("Added to the plate");

        }

        //PancakeStats.Instance.isCooked = false;
    }


}
