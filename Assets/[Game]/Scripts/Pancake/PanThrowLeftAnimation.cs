using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanThrowLeftAnimation : MonoBehaviour
{
    private Animator anim;
    public Animator cookedThrowAnim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       //if(PancakeStats.Instance.isCooked == true)
         //PanThrowAnimation();

       

    }

    //void PanThrowAnimation()
    //{
    //    if (Input.GetMouseButtonUp(0) && PancakeStats.Instance.isCooked == true)
    //    {
    //        anim.SetTrigger("IfCooked");
    //        PancakeStats.Instance.isCooked = false;
    //    }

        

        
    //}



}
