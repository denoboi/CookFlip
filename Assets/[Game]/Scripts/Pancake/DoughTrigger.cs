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
    public GameObject pancake;


    public PancakeMaterial pancakeMaterialScript;
    public CookProcess cookProcessScript;


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
        if(pancake == null)
        {
            dough.SetActive(true);
            PancakeStats.Instance.currentFace = 0;
        }
       

        //add rollbody again

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

        if (pancake == null)
        {
            pancake = Instantiate(panCakeDoughPrefab, creationPoint.transform.position, creationPoint.transform.rotation, creationPoint.transform.parent);
        }
             
        
        panAnimation.animator = pancake.GetComponent<Animator>();
        
        PancakeStats.Instance.cookingLevel[0] = 0;
        PancakeStats.Instance.cookingLevel[1] = 0;
        
        PanRollController panRollController = transform.root.GetComponentInChildren<PanRollController>();

        if (panRollController != null)
            panRollController.enabled = true;

       




        
    }

    private void Update()
    {
        //changing its parent object
        MakeChild();


    }


    void MakeChild()
    {
        if (Input.GetMouseButtonUp(0) && PancakeStats.Instance.isCooked == true)
        {
            pancake.transform.parent = plate.transform;
            Debug.Log("Plated");
            CookProcess cookProcess = pancake.GetComponentInChildren<CookProcess>();
            PancakeMaterial pancakeMaterial = pancake.GetComponentInChildren<PancakeMaterial>();
            if(cookProcess != null)
            {
                Destroy(cookProcess);
                Destroy(pancakeMaterial);
            }

          

            
        }
    }


}
