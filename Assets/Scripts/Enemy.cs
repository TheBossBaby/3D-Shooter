using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _hitPrefab;
    [SerializeField] GameObject _explosionPrefab;
    [SerializeField] int _health = 3;
    [SerializeField] int _rewardPoint;
    private Player _player;
    private NavMeshAgent _navMeshAgent;
    private int _currentHealth;

    private void OnEnable()
    {
        _currentHealth = _health;
    }

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(Vector3 impactPoint)
    {
        Instantiate(_hitPrefab, impactPoint, transform.rotation);
        _currentHealth--;
        if(_currentHealth <= 0)
        {
            Instantiate(_explosionPrefab, impactPoint, transform.rotation);
            ScoreSystem.AddPoint(_rewardPoint);
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if(player)
        {
            // Create Gameover
            SceneManager.LoadScene(0);
        }
    }
}
