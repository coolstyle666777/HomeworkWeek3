using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class RocketSpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Rocket _rocket;
    [SerializeField] private float _interval;
    [SerializeField] private float _radius;
    [SerializeField] private int _maxRockets;

    private CancellationTokenSource _cancellationTokenSource;
    private CancellationToken _cancellationToken;
    private List<Rocket> _rockets;

    private void Start()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;
        _rockets = new List<Rocket>();
        Spawn();
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }

    private async void Spawn()
    {
        while (!_cancellationToken.IsCancellationRequested)
        {
            if (_rockets.Count < _maxRockets)
            {
                Rocket rocket = Instantiate(_rocket, Random.onUnitSphere * _radius, Quaternion.identity);
                rocket.Init(_player);
                rocket.Destroyed += RemoveRocket;
                _rockets.Add(rocket);
            }
            await Task.Delay(TimeSpan.FromSeconds(_interval), _cancellationToken);
        }
    }

    private void RemoveRocket(Rocket rocket)
    {
        _rockets.Remove(rocket);
    }
}