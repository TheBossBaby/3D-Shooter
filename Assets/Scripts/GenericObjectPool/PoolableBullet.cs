using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableBullet : PooledMonobehaviour
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
        }
    }

    private bool IsLifeSpanOver()
    {
        return Time.time >= _deactivateTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        var enemy = collision.gameObject.GetComponent<PoolableEnemy>();
        if (enemy)
        {
            enemy.TakeDamage(collision.contacts[0].point);
        }
    }
}
