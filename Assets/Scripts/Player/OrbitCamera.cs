using UnityEngine;
using Mirror;

public class OrbitCamera : NetworkBehaviour
{
    [SerializeField] private float _speed = 1.5f;

    private Transform _target;
    private float _rotationY;

    void Awake()
    {
        _target = transform.parent;
        _rotationY = _target.eulerAngles.y;
    }

    void LateUpdate()
    {
        if (isLocalPlayer)
        {
            _rotationY += Input.GetAxis("Mouse X") * _speed * Time.deltaTime;
            _target.rotation = Quaternion.Euler(0, _rotationY, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
