using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro.EditorUtilities;
using UnityEngine;

public class AirplaneView : MonoBehaviour
{
    [SerializeField] private Transform _propeller;
    [SerializeField] float _rotateSpeed;

    private Tween _tween;

    private void Start()
    {
        _tween = _propeller.DOLocalRotate(new Vector3(0, 360, 0), _rotateSpeed).SetSpeedBased(true).SetEase(Ease.Linear)
            .SetRelative(true).SetLoops(-1, LoopType.Incremental).SetAutoKill(false);
    }

    public void RotatePropeller()
    {
        if (_tween != null)
        {
            if (!_tween.IsPlaying())
            {
                _tween.Play();
            }
        }
    }

    public void StopRotate()
    {
        if (_tween != null)
        {
            if (_tween.IsPlaying())
            {
                _tween.Pause();
            }
        }
    }

    private void OnDestroy()
    {
        _tween.Kill();
    }
}