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
        public bool isTutorialShowing = true;
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


    }

    private void Update()
    {

        //if (Input.GetMouseButtonUp(0))
        //{
        //    HideTutorial();
            
        //}

    }
    private void ShowTutorial()
    {
        isTutorialShowing = true;
        tutorialText.transform.DOScale(Vector3.one, .2f).SetEase(easeType);
        

        currentTargetTimeScale = Mathf.Lerp(currentTargetTimeScale, targetTimeScale, Time.unscaledDeltaTime * SPEED);
        DOSlowMotion(currentTargetTimeScale);

    }
    private void HideTutorial()
    {
        isTutorialShowing = false;
        tutorialText.SetActive(false);

        //SetAnimation();
        Time.timeScale = 1;
        Time.fixedDeltaTime = defaultFixedTime;
    }
    private void DOSlowMotion(float scale)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = Time.timeScale * 0f;
    }

    private void SetAnimation()
    {
        //tutorialText.GetComponent<Animator>().SetBool("isTutorialShowing", isTutorialShowing);
    }

}




