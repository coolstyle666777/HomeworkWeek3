using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private bool _isCollected;

    public event Action Collected;

    private void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), _rotationSpeed).SetSpeedBased(true).SetRelative(true)
            .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        AirplaneController player = other.GetComponentInParent<AirplaneController>();

        if (player != null && !_isCollected)
        {
            Collected?.Invoke();
            _isCollected = true;
            transform.DOKill();
            Destroy(gameObject);
        }
    }
}