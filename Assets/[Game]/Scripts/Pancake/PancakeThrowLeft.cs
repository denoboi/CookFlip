using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeThrowLeft : MonoBehaviour
{

    private Animator PancakeLeftAnim;

    // Start is called before the first frame update
    void Start()
    {
        PancakeLeftAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //PlateAnimation();
    }

    //public void PlateAnimation()
    //{
    //    if (Input.GetMouseButtonUp(0) && PancakeStats.Instance.isCooked == true)
    //    {
    //        PancakeLeftAnim.SetTrigger("PancakeLeftAnim");
    //        PancakeStats.Instance.isCooked = false;
    //    }

    //}
}
