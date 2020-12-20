using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
public class PoolableEnemy : PooledMonobehaviour
{
    [SerializeField] GameObject _hitPrefab;
    [SerializeField] GameObject _explosionPrefab;
    [SerializeField] AudioClip _hitSfx;
    [SerializeField] AudioClip _explosionSfx;
    [SerializeField] int _health = 3;
    [SerializeField] int _rewardPoint;
    private Player _player;
    private NavMeshAgent _navMeshAgent;
    private int _currentHealth;
    private AudioSource _audioSource;
    private Spawner _spawner;

    private void OnEnable()
    {
        _currentHealth = _health;
    }

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(Vector3 impactPoint)
    {
        Instantiate(_hitPrefab, impactPoint, transform.rotation);
        _audioSource.clip = _hitSfx;
        _audioSource?.Play();
        _currentHealth--;
        if (_currentHealth <= 0)
        {
            Instantiate(_explosionPrefab, impactPoint, transform.rotation);
            _audioSource.clip = _explosionSfx;
            _audioSource?.Play();
            ScoreSystem.AddPoint(_rewardPoint);
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }

    internal void SetSpawner(Spawner spawner)
    {
        _spawner = spawner;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            // Create Gameover
            SceneManager.LoadScene(0);
        }
    }
}
