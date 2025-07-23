using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/PowerUps List")]
public class PowerUpsListData : ScriptableObject
{
    [SerializeField] private List<GameObject> itemsPrefabs;


    public List<GameObject> ItemsPrefabs
    {
        get => itemsPrefabs;
        set => itemsPrefabs = value;
    }
}