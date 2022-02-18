using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddingToPlate : MonoBehaviour
{

    DoughTrigger doughTrigger;
    

    public Transform parent;

    private void Start()
    {
       
    }


    private void Update()
    {
        //Invoked when a button is clicked.
        //if (Input.GetMouseButtonUp(0) && PancakeStats.Instance.isCooked == true)
        //{
        //    MakeChild(parent);

        //}

        ////PancakeStats.Instance.isCooked = false;

    }


    public void MakeChild(Transform newParent)
    {
        // Sets "newParent" as the new parent of the child GameObject.
        //PanManager.Instance.CurrentPanCake.transform.SetParent(newParent);

        

        
    }
}
