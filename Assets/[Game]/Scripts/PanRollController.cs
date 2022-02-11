using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HCB.Core;

public class PanRollController : MonoBehaviour
{
    public Transform RollBody;

    private const string HIT_TWEEN_ID = "HitTweenId";
    private const float HIT_ANGLE_MAX = 25f;
    private const float HIT_ANGLE_MIN = -25f;
    private const float HIT_TWEEN_DURATION = 0.3f;

    private const float ROLL_ANGLE_LIMIT = 20f;
    private const float ROLL_ROTATE_SPEED = 10f;
    private const float RECOVERY_SPEED = 5f;

    private Vector3 _rollTargetRotation;    
    private bool _isHitTweenRunnig;
    private bool _isStarted;
    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;
        
        EventManager.OnPlayerHit.AddListener(OnHit);
        LevelManager.Instance.OnLevelStart.AddListener(() => _isStarted = true);
    }
    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;
        
        EventManager.OnPlayerHit.RemoveListener(OnHit);
        LevelManager.Instance.OnLevelStart.RemoveListener(() => _isStarted = true);
    }
    private void Update()
    {
        if (_isHitTweenRunnig || !_isStarted)
            return;

        Roll();
    }
    private void OnHit() 
    {
        if (_isHitTweenRunnig)
            return;

        _isHitTweenRunnig = true;

        int segmentCount = 4;
        float duration = HIT_TWEEN_DURATION / segmentCount;

        DOTween.Kill(HIT_TWEEN_ID);
        Sequence hitSequence = DOTween.Sequence();
        hitSequence.Append(RollBody.transform.DORotate(new Vector3(0f, 0f, HIT_ANGLE_MAX), duration).SetEase(Ease.Linear))
        .Append(RollBody.transform.DORotate(new Vector3(0f, 0f, HIT_ANGLE_MIN), duration * 2f).SetEase(Ease.Linear))
        .Append(RollBody.transform.DORotate(new Vector3(0f, 0f, 0f), duration).SetEase(Ease.Linear))
        .SetId(HIT_TWEEN_ID)
        .OnComplete(() => 
        {
            _rollTargetRotation = Vector3.zero;
            _isHitTweenRunnig = false;
        });
    }
    private void Roll() 
    {
        RollBody.localRotation = Quaternion.Slerp(RollBody.localRotation, Quaternion.Euler(_rollTargetRotation), Time.deltaTime * ROLL_ROTATE_SPEED);
        _rollTargetRotation.z = Mathf.Lerp(_rollTargetRotation.z, 0f, Time.deltaTime * RECOVERY_SPEED);
    }
    public void Rotate(Vector2 screenDelta)
    {
        //if (!Player.Instance.IsControlable)
        //    return;

        _rollTargetRotation.z -= screenDelta.x;
        _rollTargetRotation.z = Mathf.Clamp(_rollTargetRotation.z, -ROLL_ANGLE_LIMIT, ROLL_ANGLE_LIMIT);
    }
}
