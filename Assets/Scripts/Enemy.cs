using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _hitPrefab;
    [SerializeField] GameObject _explosionPrefab;
    [SerializeField] int _health = 3;
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
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }
}
