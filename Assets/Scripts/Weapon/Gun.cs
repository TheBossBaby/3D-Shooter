using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float _nextShootTime;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _delay = 0.2f;
    private Vector3 _direction;
    [SerializeField] private float _bulletSpeed;
    private Camera _camera;
    private Queue<Bullet> _pool = new Queue<Bullet>();
    public float GetBulletSpeed => _bulletSpeed;

    private void Start()
    {
        _camera = Camera.main;
    }
    private void Update()
    {
        if(Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var rayCastHit, Mathf.Infinity))
        {
            _direction = rayCastHit.point - _shootPoint.position;
            _direction.Normalize();
            _direction = new Vector3(_direction.x, 0, _direction.z);
            transform.forward = _direction;
        }
        if(CanShoot())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _nextShootTime += _delay;
        var _bullet = GetBullet();
        _bullet.transform.position = _shootPoint.position;
        _bullet.transform.rotation = _shootPoint.rotation;
        _bullet.GetComponent<Rigidbody>().velocity = _direction * _bulletSpeed;
    }

    private Bullet GetBullet()
    {
        if (_pool.Count > 0)
        {
            var bullet = _pool.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else
        {
            var bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
            bullet.SetGun(this);
            return bullet;
        }
    }
    private bool CanShoot()
    {
        return Time.time >= _nextShootTime;
    }

    public void AddToPool(Bullet bullet)
    {
        _pool.Enqueue(bullet);
    }
}
