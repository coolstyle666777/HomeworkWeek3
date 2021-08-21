using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _rollSpeed;
    [SerializeField] private float _updownSpeed;
    [SerializeField] private float _leftRightSpeed;
    [SerializeField] private float _maxForwardSpeed;
    [SerializeField] private float _forwardSpeedAcceleration;
    [SerializeField] private float _liftEpsilon;

    private AirplaneView _airplaneView;
    private float _yRotation;
    private Vector3 _playerInput;
    private float _forwardSpeed;
    private float _liftForce;
    private float _gravity;

    private void Start()
    {
        _playerInput = new Vector3();
        _airplaneView = GetComponent<AirplaneView>();
        _airplaneView.RotatePropeller();
    }

    private void Update()
    {
        _playerInput.x = Input.GetAxis("Horizontal");
        _playerInput.y = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _forwardSpeed += _forwardSpeedAcceleration;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            _forwardSpeed -= _forwardSpeedAcceleration;
        }

        _forwardSpeed = Mathf.Clamp(_forwardSpeed, 0, _maxForwardSpeed);
        if (_forwardSpeed <= 0.1f)
        {
            _airplaneView.StopRotate();
        }
        else
        {
            _airplaneView.RotatePropeller();
        }

        _liftForce = Mathf.Lerp(0, Physics.gravity.y + _liftEpsilon, _forwardSpeed / _maxForwardSpeed);
        float liftCoef =
            Mathf.Abs(Vector3.Dot(transform.forward, Vector3.ProjectOnPlane(transform.forward, Vector3.up)));
        _liftForce *= liftCoef;

        if (Input.GetKey(KeyCode.Q))
        {
            _yRotation = -1;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            _yRotation = 1;
        }
        else
        {
            _yRotation = 0;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.AddRelativeTorque(new Vector3(_playerInput.y * _updownSpeed, _yRotation * _leftRightSpeed,
            -_playerInput.x * _rollSpeed) * Time.deltaTime, ForceMode.VelocityChange);
        _rigidbody.AddRelativeForce(0, 0, _forwardSpeed * Time.deltaTime, ForceMode.VelocityChange);
        _rigidbody.AddForce(0, _liftForce, 0, ForceMode.Acceleration);
    }
}