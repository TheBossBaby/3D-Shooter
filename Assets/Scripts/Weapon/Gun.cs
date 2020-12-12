using UnityEngine;

public class Gun : MonoBehaviour
{
    private float _nextShootTime;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _delay = 0.2f;
    private Vector3 _direction;
    [SerializeField] private float _bulletSpeed;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var rayCastHit, Mathf.Infinity))
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
        var _bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
        _bullet.GetComponent<Rigidbody>().velocity = _direction * _bulletSpeed;
    }

    private bool CanShoot()
    {
        return Time.time >= _nextShootTime;
    }
}