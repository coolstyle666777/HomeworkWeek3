using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    public void Activate()
    {
        enabled = true;
    }

    private void Update()
    {
        _root.LookAt(_target);
        _root.Translate(_root.TransformDirection(_root.forward) * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        AirplaneController player = other.GetComponentInParent<AirplaneController>();
        if (player != null)
        {
            Destroy(_root.gameObject);
        }
    }
}