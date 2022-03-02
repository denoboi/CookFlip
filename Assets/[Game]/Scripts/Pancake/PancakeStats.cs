using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;


public class PancakeStats : MonoBehaviour
{

    SyrupManager syrupManager;

    private void Awake()
    {
        Instance = this;
    }


    public static PancakeStats Instance;
   

    [SerializeField] public float cookLevel = 0;

     public int[] cookingLevel = new int[] { 0, 0 };
     public int currentFace = 0;
    

     private const float MIN_COOKLEVEL = 0f;
     private const float MAX_COOKLEVEL = 70f;

    public bool isCooked;
    public bool isBurnt;
    public bool isInitialStates;

    public PanRollController panRollController;



    private void CheckStats()
    {
       //if(cookLevel > 70f)
       // {
       //     //GameManager.Instance.CompeleteStage(false);
       // }
    }

    
    public void ChangeFace()
    {
        if (currentFace == 0)
            currentFace = 1;

        else
            currentFace = 0;
    }

    public void CookFace()
    {
        cookingLevel[currentFace] += 10;



        CheckCooking();
        {
            EventManager.OnPancakeCooked.Invoke();

          

            //HapticManager.Haptic(HapticTypes.RigidImpact);
            HapticManager.Haptic(HapticTypes.HeavyImpact);
            //syrupManager.isOnPlate = true;

        }

        CheckBurnt();

      
    }

    private bool CheckCooking()
    {
        if(cookingLevel[0] >= 30 & cookingLevel[0] <=70 && cookingLevel[1] >= 30 & cookingLevel[1] <= 70)
        {
            return isCooked = true; 
        }

        else
        {
            return isCooked = false;
        }

    }

    private bool CheckBurnt()
    {
        if (cookingLevel[0] > 70 && cookingLevel[1] > 70 || cookingLevel[0] >= 30 && cookingLevel[1] > 70 || cookingLevel[1] >= 30 && cookingLevel[0] > 70)
        {
            return isBurnt = true;
        }

        else
        {
            return isBurnt = false;
        }
    }





}
