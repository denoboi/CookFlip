using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTrigger : MonoBehaviour
{
    [SerializeField] private GameObject pancake;
    [SerializeField] private float delay = 2f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cooked");
        EnablePancake();

    }

    public void EnablePancake()
    {
        StartCoroutine(EnableCo(pancake));
    }

    private IEnumerator EnableCo(GameObject pancake)
    {
        if(!pancake.activeSelf)
        {
            yield return new WaitForSeconds(delay);
            pancake.SetActive(true);
        }

        
    }


}
