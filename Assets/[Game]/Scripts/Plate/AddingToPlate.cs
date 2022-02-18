using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddingToPlate : MonoBehaviour
{
    public GameObject child;

    public Transform parent;


    private void Update()
    {
        //Invoked when a button is clicked.
        if(Input.GetMouseButtonUp(0) && PancakeStats.Instance.isCooked == true)
        {
            MakeChild(parent);
        }

    }


    public void MakeChild(Transform newParent)
    {
        // Sets "newParent" as the new parent of the child GameObject.
        child.transform.SetParent(newParent);

        

        
    }
}
