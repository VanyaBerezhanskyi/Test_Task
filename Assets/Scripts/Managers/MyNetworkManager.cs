using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    [SerializeField] private Transform[] _playersSpawns;

    public GameObject mainCamera;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        int spawnIndex = conn.connectionId % _playersSpawns.Length;
        GameObject player = Instantiate(playerPrefab, _playersSpawns[spawnIndex].position, _playersSpawns[spawnIndex].rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnStartClient()
    {
        mainCamera.SetActive(false);
    }

    public override void OnStopClient()
    {
        mainCamera.SetActive(true);
    }
}
