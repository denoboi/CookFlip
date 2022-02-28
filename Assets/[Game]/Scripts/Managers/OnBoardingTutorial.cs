using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;
using DG.Tweening;


public class OnBoardingTutorial : MonoBehaviour
{
    
        [SerializeField] GameObject tutorialText;
        private float currentTargetTimeScale = 1f;
        private float targetTimeScale = 0.1f;
        private const float SPEED = 100f;

        private float defaultFixedTime;
        private bool isTutorialShowing = false;
        [SerializeField] Ease easeType;

    DoughTrigger DoughTrigger;
    OvenTrigger OvenTrigger;
    SplineCharacter SplineCharacter;




    public DoughTrigger doughTrigger => DoughTrigger == null ? DoughTrigger = GetComponentInChildren<DoughTrigger>() : DoughTrigger;

    public OvenTrigger ovenTrigger => OvenTrigger == null ? OvenTrigger = GetComponentInChildren<OvenTrigger>() : OvenTrigger;

    

    


    private void Awake()
        {
            Time.timeScale = 1;
            defaultFixedTime = Time.fixedDeltaTime;
            
            

    }

    private void OnEnable()
    {
            //if (GameManager.Instance.GameConfig.IsLooping)
                //Destroy(gameObject);
    }

        private void OnTriggerEnter(Collider other)
        {
            SplineCharacter splineCharacter = other.GetComponentInParent<SplineCharacter>();
        

            if (splineCharacter == null) return;

            Debug.Log("splineCharacter");

            ShowTutorial();

       

        }



    private void OnTriggerExit(Collider other)
    {

        SplineCharacter splineCharacter = other.GetComponentInParent<SplineCharacter>();

        if (splineCharacter == null) return;

        HideTutorial();

        tutorialText.SetActive(false);


    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            HideTutorial();
        }
       

    }
    private void ShowTutorial()
    {

        if (isTutorialShowing) return;

        isTutorialShowing = true;
        tutorialText.transform.DOScale(Vector3.one, .8f).SetEase(easeType).SetUpdate(true);
        

        currentTargetTimeScale = Mathf.Lerp(currentTargetTimeScale, targetTimeScale, Time.unscaledDeltaTime * SPEED);
        //DOSlowMotion(0);
        Time.timeScale = 0;
        

        EventManager.OnMovementStop.Invoke();




    }
    private void HideTutorial()
    {
        //isTutorialShowing = false;

        EventManager.OnMovementStart.Invoke();
        //SetAnimation();
        Time.timeScale = 1;
        Time.fixedDeltaTime = defaultFixedTime;
        GameObject.FindObjectOfType<FloatingJoystick>().gameObject.SetActive(true);
    }
    private void DOSlowMotion(float scale)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = Time.timeScale * .5f;
    }

    private void SetAnimation()
    {
        //tutorialText.GetComponent<Animator>().SetBool("isTutorialShowing", isTutorialShowing);
    }

}




