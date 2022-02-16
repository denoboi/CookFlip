using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

public class CookedThrowAnim : MonoBehaviour
{
    //private void OnEnable()
    //{
    //    if (Managers.Instance == null) return;

    //    EventManager.OnPancakeCooked.AddListener(throwAnim);
    //}

    //private void OnDisable()
    //{
    //    if (Managers.Instance == null) return;

    //    EventManager.OnFinishTrigger.RemoveListener();
    //}
    
    public Animator throwAnim;
    

    // Start is called before the first frame update
    void Start()
    {
        throwAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        throwAnim.SetTrigger(1);
    }

    void ThrowAnim()
    {
        //throwAnim.Set
    }
}
