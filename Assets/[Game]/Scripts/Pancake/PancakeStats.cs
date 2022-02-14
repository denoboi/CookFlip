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
    [SerializeField] public GameObject pancakeFront;
    [SerializeField] public GameObject pancakeBack;

    private const float MIN_COOKLEVEL = 0f;
    private const float MAX_COOKLEVEL = 70f;
    
    

    private void CheckStats()
    {
       if(cookLevel > 70f)
        {
            GameManager.Instance.CompeleteStage(false);
        }
    }

}
