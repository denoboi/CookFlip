using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{

    public static StackManager instance;

    [SerializeField] private float _distanceBetweenObjects;
    [SerializeField] public GameObject _prevObject;
    [SerializeField] public Transform _parent;

    public GameObject CurrentPanCake;
    
    


    public List<GameObject> pancakeList = new List<GameObject>();



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
   
    public void StackList()
    {

        //StartCoroutine(CallStack());

    }

    public IEnumerator CallStack()
    {
        yield return new WaitForSeconds(2f);
        _prevObject.transform.localPosition = new Vector3(0, 5f * pancakeList.Count, 0);
        pancakeList.Add(_prevObject);
        
    }

    public int StackCount()
    {
        return pancakeList.Count;
    }

    public void AddStack(GameObject stack)
    {
        if (pancakeList.Contains(stack))
            return;
        pancakeList.Add(stack);
    }



}
