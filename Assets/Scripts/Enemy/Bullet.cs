using UnityEngine;
using Mirror;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float _damage = 10;
    [SerializeField] private float _speed = 5;

    private void Update()
    {
        if (!isServer) return;

        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isServer) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            NetworkConnectionToClient targetConnection = collision.gameObject.GetComponent<NetworkIdentity>().connectionToClient;
            Messenger<NetworkConnection, float>.Broadcast(GameEvent.PLAYER_HIT, targetConnection, _damage);
        }

        NetworkServer.Destroy(gameObject);
    }
}
