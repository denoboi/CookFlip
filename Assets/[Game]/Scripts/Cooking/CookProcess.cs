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
        PancakeStats.Instance.cookLevel += 10;
        Debug.Log(PancakeStats.Instance.cookLevel);

        if (PancakeStats.Instance.cookLevel >= 30)
        {
            Debug.Log("cooked");

            pancakeMaterialScript.ChangeCooked();


        }
        if (PancakeStats.Instance.cookLevel >= 70)
        {
            Debug.Log("burned");

            pancakeMaterialScript.ChangeBurnt();
        }
    }

   


   


   }

