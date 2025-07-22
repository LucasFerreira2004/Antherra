using UnityEngine;

[CreateAssetMenu(menuName = "Player/Status Data")]
public class PlayerHealthData : ScriptableObject
{
    public int MaxHealth;
    public int CurrentHealth;

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }

}
