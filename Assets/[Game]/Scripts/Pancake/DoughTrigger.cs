using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public bool isDoughAvailable;

    void Start()
    {
        DOTween.Init();
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("DoughSpilled");
        if (other.CompareTag("PancakeDoughBone") && isDoughAvailable == false)
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

        //PancakeStats.Instance.isCooked = false;

        PancakeStats.Instance.currentFace = 0;

        PancakeStats.Instance.cookingLevel[0] = 0;
        PancakeStats.Instance.cookingLevel[1] = 0;
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
        isDoughAvailable = true;

        StackManager.instance.CurrentPanCake = pancake;
        
        panAnimation.animator = pancake.GetComponent<Animator>();
        

        //add rollbody again
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
        if (Input.GetMouseButtonUp(0) && PancakeStats.Instance.isCooked == true || PancakeStats.Instance.isBurnt)
        {


            //transform.SetParent(StackManager.instance._parent);
            //transform.DOLocalJump(new Vector3(0, .5f * StackManager.instance.StackCount(), 0), 0.2f, 1, 1f);
            //StackManager.instance.AddStack(gameObject);

            //StackManager.instance.StackList();
            //Debug.Log("EventTrggered");


            //pancake.transform.parent = plate.transform;


            isDoughAvailable = false;
            
            
            Debug.Log("Plated");
            
            CookProcess cookProcess = pancake.GetComponentInChildren<CookProcess>();
            PancakeMaterial pancakeMaterial = pancake.GetComponentInChildren<PancakeMaterial>();
            

            if(cookProcess != null)
            {
                Destroy(cookProcess);
                Destroy(pancakeMaterial);

            }

           

            //if(pancake.transform.parent == plate.transform)
            //{
            //    //Destroy(pancake.GetComponent<Animator>());
            //}


            //PancakeStats.Instance.isCooked = false;

        }



    }


}
