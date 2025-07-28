using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomManager : MonoBehaviour, IRoomManager
{
    [SerializeField] private List<Transform> enemySpawnPoints;
    [SerializeField] private List<GameObject> enemyPrefabs;

    private List<GameObject> activeEnemies = new List<GameObject>();
    public void ActivateRoom()
    {
        bool enemySpawsNullOrEmpty = (enemySpawnPoints == null || enemySpawnPoints.Count == 0);
        bool enemyPrefabsNullOrEmpty = (enemyPrefabs == null || enemyPrefabs.Count == 0);

        if (enemySpawsNullOrEmpty || enemyPrefabsNullOrEmpty)
        {
            EnableEntrances(true);
            return;
        }
        SpawnEnemies();
        EnableEntrances(false);
    }

    private void SpawnEnemies()
    {
        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Count);
            GameObject enemy = Instantiate(enemyPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
            IEnemyTakeDamage genericEnemy = enemy.GetComponent<IEnemyTakeDamage>();
            genericEnemy.OnEnemyDeath += CheckRoomClear;
            activeEnemies.Add(enemy);
        }

        // Inicia música de batalha
        AudioController.Instance?.SetBattleSong();
    }


    private void CheckRoomClear(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        if (activeEnemies.Count == 0)
        {
            EnableEntrances(true);
            // Retorna à música normal
            AudioController.Instance?.RemoveBattleSong();
        }
    }

    private void EnableEntrances(bool open)
    {
        foreach (var component in GetComponentsInChildren<MonoBehaviour>(true)) // 'true' inclui objetos inativos
        {
            if (component is IRoomEntrance entrance)
            {
                entrance.SetOpen(open);
            }
        }
    }
}
