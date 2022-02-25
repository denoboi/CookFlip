using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.SplineMovementSystem;


public class OnBoardingTutorial : MonoBehaviour
{
    
        [SerializeField] GameObject tutorialText;
        private float currentTargetTimeScale = 1f;
        private float targetTimeScale = .5f;
        private const float SPEED = 10f;

        private float defaultFixedTime;
        private bool isTutorialShowing = false;

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
                 Debug.Log("doughtut");
           
        }



    private void OnTriggerExit(Collider other)
    {

        SplineCharacter splineCharacter = other.GetComponentInParent<SplineCharacter>();

        if (splineCharacter == null) return;
        
                HideTutorial();
                
            
        
    }
    private void Update()
    {
        if (!isTutorialShowing) return;

        currentTargetTimeScale = Mathf.Lerp(currentTargetTimeScale, targetTimeScale, Time.unscaledDeltaTime * SPEED);
        DOSlowMotion(currentTargetTimeScale);
    }
    private void ShowTutorial()
    {
        isTutorialShowing = true;
        tutorialText.SetActive(true);
    }
    private void HideTutorial()
    {
        isTutorialShowing = false;
        SetAnimation();
        Time.timeScale = 1;
        Time.fixedDeltaTime = defaultFixedTime;
    }
    private void DOSlowMotion(float scale)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    private void SetAnimation()
    {
        //tutorialText.GetComponent<Animator>().SetBool("isTutorialShowing", isTutorialShowing);
    }

}




