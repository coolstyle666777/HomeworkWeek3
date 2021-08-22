using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private float _speed;
    [SerializeField] private float _force;
    [SerializeField] private float _radius;

    private Rigidbody _rigidbody;
    private Transform _target;

    public event Action<Rocket> Destroyed;

    public void Init(Transform target)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _target = target;
        enabled = true;
    }

    private void Update()
    {
        transform.LookAt(_target);
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        AirplaneController player = other.gameObject.GetComponentInParent<AirplaneController>();
        if (player != null)
        {
            player.GetComponent<Rigidbody>().AddExplosionForce(_force, other.GetContact(0).point, _radius);
            Instantiate(_explosion, other.GetContact(0).point, Quaternion.identity);
            Destroy(gameObject);
            Destroyed?.Invoke(this);
        }
    }
}