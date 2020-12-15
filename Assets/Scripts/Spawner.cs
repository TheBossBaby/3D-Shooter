using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float _nextSpwanTime;
    [SerializeField] float _delay;
    [SerializeField] Transform[] _spwanPoints;
    [SerializeField] Enemy[] _enemies;

    void Update()
    {
        if (ShouldSpawn())
            Spawn();
    }

    private void Spawn()
    {
        _nextSpwanTime = Time.time + _delay;
        Transform spwanPoint = GetSpawnPoint();
        Enemy enemy = GetEnemy();
        Instantiate(enemy, spwanPoint.position, spwanPoint.rotation);
    }

    private Transform GetSpawnPoint()
    {
        var randomIndex = UnityEngine.Random.Range(0, _spwanPoints.Length);
        var spwanPoint = _spwanPoints[randomIndex];
        return spwanPoint;
    }

    private Enemy GetEnemy()
    {
        var randomIndex = UnityEngine.Random.Range(0, _enemies.Length);
        var enemy = _enemies[randomIndex];
        return enemy;
    }

    private bool ShouldSpawn()
    {
        return Time.time >= _nextSpwanTime;
    }
}
