using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTrigger : MonoBehaviour
{
    [SerializeField] private GameObject pancake;
    [SerializeField] private float delay = 2f;
    [SerializeField] private float cookLevel = 0;
    private Coroutine cookingCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cooking");
        //EnablePancake();
        Cooking();
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(cookingCoroutine);
    }


    

    //public void EnablePancake()
    //{
    //    StartCoroutine(EnableCo(pancake));
    //}


    public void Cooking()
    {
        cookingCoroutine = StartCoroutine(CookingLevel());
    }

    private IEnumerator EnableCo(GameObject pancake)
    {
        if(!pancake.activeSelf)
        {
            yield return new WaitForSeconds(delay);
            pancake.SetActive(true);
        }

        
    }

    private IEnumerator CookingLevel()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            PancakeStats.Instance.cookLevel += 10;
            Debug.Log(PancakeStats.Instance.cookLevel);

            if (PancakeStats.Instance.cookLevel >= 50)
            {
                
                Debug.Log("cooked");
            }
            if(PancakeStats.Instance.cookLevel >= 70)
            {
                Debug.Log("burned");
            }

        }
            
        
    }


}
