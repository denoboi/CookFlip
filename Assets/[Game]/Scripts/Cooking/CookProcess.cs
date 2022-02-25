using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

public class CookProcess : MonoBehaviour
{
   
    public PancakeMaterial pancakeMaterialScript;


    private void OnEnable()
    {
        EventManager.OnPancakeCooking.AddListener(CookingCakes);
    }

    private void OnDisable()
    {
        EventManager.OnPancakeCooking.RemoveListener(CookingCakes);
    }

    void Start()
    {
        pancakeMaterialScript = transform.root.GetComponentInChildren<PancakeMaterial>();
    }

   



    public void CookingCakes()
    {
        PancakeStats.Instance.CookFace();
        

        Debug.Log(PancakeStats.Instance.cookLevel);

        if (PancakeStats.Instance.cookingLevel[PancakeStats.Instance.currentFace] >= 30 && PancakeStats.Instance.currentFace == 0)
        {
            Debug.Log("cooked");

            pancakeMaterialScript.CookedUp();

            HapticManager.Haptic(HapticTypes.RigidImpact);

        }

        else if(PancakeStats.Instance.cookingLevel[PancakeStats.Instance.currentFace] >= 30 && PancakeStats.Instance.currentFace == 1)
        {
            pancakeMaterialScript.CookedDown();

            HapticManager.Haptic(HapticTypes.RigidImpact);
        }

        if (PancakeStats.Instance.cookingLevel[PancakeStats.Instance.currentFace] > 70 && PancakeStats.Instance.currentFace == 0) 
        {
            Debug.Log("burned");
            ParticleManager.instance.BurntParticleMethod();

            HapticManager.Haptic(HapticTypes.SoftImpact);

            pancakeMaterialScript.BurntUp();
        }

        else if(PancakeStats.Instance.cookingLevel[PancakeStats.Instance.currentFace] > 70 && PancakeStats.Instance.currentFace == 1 )
        {
            pancakeMaterialScript.BurntDown();

            HapticManager.Haptic(HapticTypes.SoftImpact);

            ParticleManager.instance.BurntParticleMethod();
        }
    }

   
    
    
             

   }

