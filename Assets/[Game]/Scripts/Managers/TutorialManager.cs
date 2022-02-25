using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;

    DoughTrigger DoughTrigger;
    OvenTrigger OvenTrigger;

    public DoughTrigger doughTrigger => DoughTrigger == null ? DoughTrigger = GetComponentInChildren<DoughTrigger>() : DoughTrigger;
    public OvenTrigger ovenTrigger => OvenTrigger == null ? OvenTrigger = GetComponentInChildren<OvenTrigger>() : OvenTrigger;


    private void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            }
            else
            {
                popUps[popUpIndex].SetActive(false);
            }
            
        }


        if(popUpIndex == 0)
        {
            if(doughTrigger.isDoughAvailable)
            {
                popUpIndex++;
            }
            else if(popUpIndex == 1)
            {
                if(ovenTrigger.isExited && Input.GetMouseButtonUp(0))
                {
                    popUpIndex++;
                }
            }
            else if(popUpIndex == 2)
            {
                if(PancakeStats.Instance.isCooked && Input.GetMouseButtonUp(0))
                {
                    popUpIndex++;
                }
            }
        }
    }
}
