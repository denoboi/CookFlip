using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeFlip : MonoBehaviour
{
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CakeFlipAnimation();
    }

    void CakeFlipAnimation()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("cakeflip");
            anim.SetBool("CakeFlip", true);

           


        }
        else if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("CakeFlip", false);
        }
    }
}
