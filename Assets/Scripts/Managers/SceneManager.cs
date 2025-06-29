using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class SceneManager : NetworkBehaviour
{
    [SerializeField] private GameObject _enemy;

    private void Start()
    {
        if (!isServer) return;

        InvokeRepeating(nameof(SpawnEnemy), 1f, 5f);
    }

    private void SpawnEnemy()
    {
        while (true)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-10, 10), 0.75f, Random.Range(-10, 10));

            Collider[] colliders = Physics.OverlapSphere(spawnPoint, 0.45f);

            if (colliders.Length == 1) // Якщо сфера зіштовхнулась тільки з землею - спавнимо
            {
                GameObject enemy = Instantiate(_enemy, spawnPoint, Quaternion.identity);
                NetworkServer.Spawn(enemy);

                break;
            }
        }
    }
}
