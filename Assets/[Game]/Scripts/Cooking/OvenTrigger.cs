using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _pancakeFront;
    [SerializeField] private GameObject _pancakeBack;
    [SerializeField] private float _delay = 2f;
    
    private Coroutine _cookingCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cooking");
        
        if(other.gameObject.CompareTag("Pan"))
        {
            Cooking();
        }
        
            
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(_cookingCoroutine);
    }



    public void Cooking()
    {
        _cookingCoroutine = StartCoroutine(CookingLevel());
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
            if(PancakeStats.Instance.cookLevel >= 90)
            {
                Debug.Log("burned");
            }

        }
            
        
    }


}
