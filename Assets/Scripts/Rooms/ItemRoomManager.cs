using System.Collections.Generic;
using UnityEngine;

public class ItemRoomManager : MonoBehaviour, IRoomManager
{
    [SerializeField] private List<Transform> itemsSpawnPoints;
    [SerializeField] private PowerUpsListData ItemsPrefabsData;

    private List<GameObject> ItemsPrefabs; 
    
    public void ActivateRoom()
    {
        Debug.Log("chamado");
        ItemsPrefabs = ItemsPrefabsData.ItemsPrefabs;
        bool itemsSpawnPointsNullOrEmpty = (itemsSpawnPoints == null || itemsSpawnPoints.Count == 0);
        bool ItemsPrefabsNullOrEmpty = (ItemsPrefabs == null || ItemsPrefabs.Count == 0);

        if (itemsSpawnPointsNullOrEmpty || ItemsPrefabsNullOrEmpty)
            return;

        Debug.Log("chamado2");
        SpawnItems();
        EnableEntrances(true);
    }

    private void SpawnItems()
    {
        foreach (Transform spawnPoint in itemsSpawnPoints)
        {
            int randomIndex = Random.Range(0, ItemsPrefabs.Count);
            Instantiate(ItemsPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
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
