using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float _nextSpwanTime;
    [SerializeField] float _delay;
    [SerializeField] Transform[] _spwanPoints;
    [SerializeField] Enemy[] _enemies;
    [SerializeField] PoolableEnemy[] _poolableEnemies;
    private Queue<Enemy> _enemyPool = new Queue<Enemy>();

    void Update()
    {
        if (ShouldSpawn())
            Spawn();
    }

    private void Spawn()
    {
        _nextSpwanTime = Time.time + _delay;
        Transform spwanPoint = GetSpawnPoint();
        //Enemy enemy = GetEnemy();
        PoolableEnemy poolableEnemyPrefab = GetPoolableEnemy();
        var pe = poolableEnemyPrefab.Get<PoolableEnemy>(spwanPoint.position, spwanPoint.rotation);
        //var e = GetEnemy(enemy, spwanPoint);
        //e.transform.position = spwanPoint.position;
        //e.transform.rotation = spwanPoint.rotation;
        //Instantiate(enemy, spwanPoint.position, spwanPoint.rotation);
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

    private PoolableEnemy GetPoolableEnemy()
    {
        var randomIndex = UnityEngine.Random.Range(0, _enemies.Length);
        var enemy = _poolableEnemies[randomIndex];
        return enemy;
    }

    private bool ShouldSpawn()
    {
        return Time.time >= _nextSpwanTime;
    }

    private Enemy GetEnemy(Enemy enemyPrefab, Transform spawnPoint)
    {
        if (_enemyPool.Count > 0)
        {
            var enemy = _enemyPool.Dequeue();
            enemy.gameObject.SetActive(true);
            return enemy;
        }
        else
        {
            var enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemy.SetSpawner(this);
            return enemy;
        }
    }

    public void AddToPool(Enemy enemy)
    {
        _enemyPool.Enqueue(enemy);
    }
}
