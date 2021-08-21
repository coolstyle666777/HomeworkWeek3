using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIArrow : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private List<Coin> _coins;
    private float _closestDistance;

    public float ClosestDistance => _closestDistance;

    public void Show(List<Coin> coins)
    {
        _coins = coins;
        enabled = true;
    }

    public void Hide()
    {
        enabled = false;
    }

    private void FixedUpdate()
    {
        transform.forward = GetClosestCoinDirection(_coins).Item1;
    }

    private (Vector3, float) GetClosestCoinDirection(List<Coin> coins)
    {
        Coin closest = coins[0];
        _closestDistance = float.MaxValue;
        coins.ForEach(coin =>
        {
            if (coin != null)
            {
                float distance = Vector3.Distance(_player.position, coin.transform.position);
                if (distance < _closestDistance)
                {
                    _closestDistance = distance;
                    closest = coin;
                }
            }
        });
        return ((closest.transform.position - _player.position).normalized, _closestDistance);
    }
}