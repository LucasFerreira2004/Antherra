using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/PowerUps List")]
public class PowerUpsListData : ScriptableObject
{
    [SerializeField] private List<GameObject> itemsPrefabs;
    [SerializeField] private List<PowerUpsListData> otherLists;

    public void Awake()
    {
        foreach (PowerUpsListData list in otherLists) {
            foreach (var itemPerfab in list.ItemsPrefabs)
            {
                itemsPrefabs.Add(itemPerfab);
            }
        }
    }
    public List<GameObject> ItemsPrefabs
    {
        get => itemsPrefabs;
        set => itemsPrefabs = value;
    }
}