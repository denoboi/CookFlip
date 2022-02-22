using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PancakeThrowLeft : MonoBehaviour
{

    private Animator PancakeLeftAnim;

    [SerializeField] private Transform plate;

    // Start is called before the first frame update
    void Start()
    {
        PancakeLeftAnim = GetComponent<Animator>();
        DOTween.Init();
    }


    void Update()
    {
        
        PlateAnimation();
    }

    //public void PlateAnimation()
    //{
    //    if (Input.GetMouseButtonUp(0) && PancakeStats.Instance.isCooked == true)
    //    {
    //        PancakeLeftAnim.SetTrigger("PancakeLeftAnim");
    //        PancakeStats.Instance.isCooked = false;
    //    }

    //}

    public void PlateAnimation()
    {
        if (StackManager.instance.CurrentPanCake == null)
            return;

        if (StackManager.instance.CurrentPanCake != this.gameObject)
            return;


        if (Input.GetMouseButtonUp(0) && PancakeStats.Instance.isCooked == true || PancakeStats.Instance.isBurnt == true)
        {
            transform.SetParent(StackManager.instance._parent);
            StackManager.instance.AddStack(gameObject);
            transform.DOLocalJump(new Vector3(0, 0, 0.085f * StackManager.instance.StackCount()), 1.5f, 1, 0.5f);
            StackManager.instance.CurrentPanCake = null;

        }

    }

    //public void PanCakeLeftEvent()
    //{
    //    StackManager.instance.StackList();
    //    Debug.Log("EventTrggered");
    //}
}
