using UnityEngine;

[CreateAssetMenu(menuName = "Player/Status Data")]
public class PlayerHealthData : ScriptableObject
{
     [Header("Standard values")]
    [SerializeField] private int defaultMaxHealth = 6;

    public int CurrentMaxHealth;
    public int CurrentHealth;

    public void ResetHealth()
    {
        CurrentMaxHealth = defaultMaxHealth;
        CurrentHealth = CurrentMaxHealth;
    }

}
