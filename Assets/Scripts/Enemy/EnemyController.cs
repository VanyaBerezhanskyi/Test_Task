using Mirror;
using UnityEngine;

public class EnemyContoller : NetworkBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _shootRate = 3;
    [SerializeField] private float _attackRadius = 5;
    [SerializeField] private float _rotationSpeed = 25;

    private float _elapsedTime = 0;
    private Transform _bulletSpawnPoint;
    private bool _targetFound = false;

    private void Awake()
    {
        _bulletSpawnPoint = transform.GetChild(0);
    }


    private void Update()
    {
        if (!isServer) return;

        if (!_targetFound)
        {
            transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
        }

        if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out RaycastHit hit, _attackRadius))
        {
            if (hit.transform.CompareTag("Player"))
            {
                transform.LookAt(hit.transform.position);

                if (_elapsedTime > _shootRate)
                {
                    GameObject bullet = Instantiate(_bullet, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
                    NetworkServer.Spawn(bullet);

                    _elapsedTime = 0;
                }

                _targetFound = true;
            }
        }
        else
            _targetFound = false;

        _elapsedTime += Time.deltaTime;
    }
}
