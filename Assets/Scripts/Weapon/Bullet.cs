using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Gun _gun;
    public void SetGun(Gun gun) => _gun = gun;
    private float _bulletLifeTime;
    private float _deactivateTime;
    [SerializeField] private float _maximumDistance;

    private void Start()
    {
        _bulletLifeTime = _maximumDistance / _gun.GetBulletSpeed;
    }

    private void OnEnable()
    {
        _deactivateTime = Time.time + _bulletLifeTime;
    }

    private void Update()
    {
        if (IsLifeSpanOver())
        {
            gameObject.SetActive(false);
            _gun.AddToPool(this);
        }
    }

    private bool IsLifeSpanOver()
    {
        return Time.time >= _deactivateTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        _gun.AddToPool(this);

        var enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy)
        {
            enemy.TakeDamage(collision.contacts[0].point);
        }
    }
} 