using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private Transform[] enemySpawnPoints;
    [SerializeField] private List<GameObject> enemyPrefabs;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private bool playerInside = false;

    public void ActivateRoom()
    {
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
    }


    private void CheckRoomClear(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        if (activeEnemies.Count == 0)
        {
            EnableEntrances(true);
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
