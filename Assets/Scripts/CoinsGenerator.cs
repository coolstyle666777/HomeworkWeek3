using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private int _coinsCount;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _radius;

    public List<Coin> Generate()
    {
        List<Coin> coins = new List<Coin>();
        for (int i = 0; i < _coinsCount; i++)
        {
            GameObject coin = Instantiate(_coinPrefab,
                _spawnPoint.position + Random.insideUnitSphere * _radius,
                Quaternion.identity);

            coins.Add(coin.GetComponentInChildren<Coin>());
        }

        return coins;
    }
}