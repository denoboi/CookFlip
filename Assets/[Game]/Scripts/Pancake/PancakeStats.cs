using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;


public class PancakeStats : MonoBehaviour
{


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
    
    

    private void CheckStats()
    {
       if(cookLevel > 70f)
        {
            GameManager.Instance.CompeleteStage(false);
        }
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
        
        
        
        if(CheckCooking())
        {
            EventManager.OnPancakeCooked.Invoke();
        }
    }

    private bool CheckCooking()
    {
        if(cookingLevel[0] >= 100 && cookingLevel[1] >= 100)
        {
            
            return isCooked = true;
        }

        else
        {
            return false;
        }

        
        
    }

}
