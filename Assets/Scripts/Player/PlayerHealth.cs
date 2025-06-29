using Mirror;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    private float _health = 100;
    private MyNetworkManager _networkManager;

    private void Awake()
    {
        Messenger<NetworkConnection, float>.AddListener(GameEvent.PLAYER_HIT, RpcOnPlayerHit);
    }

    private void OnDestroy()
    {
        Messenger<NetworkConnection, float>.RemoveListener(GameEvent.PLAYER_HIT, RpcOnPlayerHit);
    }

    private void Start()
    {
        _networkManager = FindAnyObjectByType<MyNetworkManager>();
    }

    [TargetRpc]
    private void RpcOnPlayerHit(NetworkConnection conn, float damage)
    {
        _health -= damage;

        if (_health == 0)
        {
            _networkManager.mainCamera.SetActive(true);

            CmdRemovePlayer();
        }

        Messenger<float>.Broadcast(GameEvent.HEALTH_CHANGED, _health);
    }

    [Command]
    private void CmdRemovePlayer()
    {
        NetworkServer.Destroy(gameObject);
    }
}
