using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float _speed = 10;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            float deltaX = Input.GetAxis("Horizontal");
            float deltaZ = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(deltaX, 0, deltaZ) * _speed * Time.fixedDeltaTime;

            direction = Vector3.ClampMagnitude(direction, _speed); // Обмежеуємо рух по діагоналі

            direction = transform.TransformDirection(direction);

            _rigidBody.MovePosition(transform.position + direction);
        }
    }
}
