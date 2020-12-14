using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float _nextSpwanTime;
    [SerializeField] float _delay;
    [SerializeField] GameObject _prefab;
    [SerializeField] Transform[] _spwanPoints;

    void Update()
    {
        if (ShouldSpawn())
            Spawn();
    }

    private void Spawn()
    {
        _nextSpwanTime = Time.time + _delay;
        var randomIndex = UnityEngine.Random.Range(0, _spwanPoints.Length);
        var spwanPoint = _spwanPoints[randomIndex];
        Instantiate(_prefab, spwanPoint.position, spwanPoint.rotation);
    }

    private bool ShouldSpawn()
    {
        return Time.time >= _nextSpwanTime;
    }
}
