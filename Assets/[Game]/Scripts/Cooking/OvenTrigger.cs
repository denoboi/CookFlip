using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

public class OvenTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _pancakeFront;
    [SerializeField] private GameObject _pancakeBack;
    [SerializeField] private float _delay = 2f;

    private Coroutine _cookingCoroutine;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cooking");

        if (other.gameObject.CompareTag("Pan"))
        {
            Cooking();
            HeatParticleColorController.instance.OnParticleEnter();

        }


    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(_cookingCoroutine);
        HeatParticleColorController.instance.OnParticleExit();
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
            EventManager.OnPancakeCooking.Invoke();

        }


    }

}
