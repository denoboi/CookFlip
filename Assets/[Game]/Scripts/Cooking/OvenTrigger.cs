using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;

public class OvenTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _pancakeFront;
    [SerializeField] private GameObject _pancakeBack;
    [SerializeField] private float _delay = 2f;

    private Coroutine _cookingCoroutine;

    SplineCharacterMovementController controller;

    public bool isExited;




    private HeatParticleColorController[] HeatParticleColorController;
    private HeatParticleColorController[] particleColorControllers => HeatParticleColorController == null ? HeatParticleColorController = GetComponentsInChildren<HeatParticleColorController>() : HeatParticleColorController;

    private void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {

        PancakeStats stats = other.transform.root.GetComponentInChildren<PancakeStats>();
        controller = stats.GetComponent<SplineCharacterMovementController>();

        Debug.Log("Cooking");

        if (other.gameObject.CompareTag("Pan"))
        {
            Cooking();
            //HapticManager.Haptic(HapticTypes.RigidImpact);
            controller.SetSpeed(controller.MovementData.CookingSpeed);
            ParticleManager.instance.SmokeParticleMethod();

        }


    }

    public void OnTriggerExit(Collider other)
    {

        if(other.gameObject.CompareTag("Pan"))
        {
            StopCoroutine(_cookingCoroutine);

            controller.SetSpeed(controller.MovementData.DefaultSpeed);
            ParticleManager.instance.SmokeParticleMethodStop();
            ParticleManager.instance.BurntParticleMethodStop();

            isExited = true;
        }
        
        
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
