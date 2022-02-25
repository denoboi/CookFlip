using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

public class OnBoardingTutorial : MonoBehaviour
{
    
        [SerializeField] GameObject tutorialText;
        private float currentTargetTimeScale = 1f;
        private float targetTimeScale = .5f;
        private const float SPEED = 10f;

        private float defaultFixedTime;
        private bool isTutorialShowing = false;

    DoughTrigger DoughTrigger;
    public DoughTrigger doughTrigger => DoughTrigger == null ? DoughTrigger = GetComponentInChildren<DoughTrigger>() : DoughTrigger;
   
        private void Awake()
        {
            Time.timeScale = 1;
            defaultFixedTime = Time.fixedDeltaTime;
        }

        private void OnEnable()
        {
            if (GameManager.Instance.GameConfig.IsLooping)
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
        DoughTrigger doughTrigger = other.GetComponent<DoughTrigger>();
            if (doughTrigger)
            {
                if (doughTrigger.isDoughAvailable)
                {
                    ShowTutorial();
                }
            }
        }

        //private void OnTriggerExit(Collider other)
        //{
        //    Character character = other.GetComponent<Character>();
        //    if (character)
        //    {
        //        if (character.CharacterData.CharacterControlType == CharacterControlType.Player)
        //        {
        //            HideTutorial();
        //        }
        //    }
        //}
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
            tutorialText.GetComponent<Animator>().SetBool("isTutorialShowing", isTutorialShowing);
        }
}


