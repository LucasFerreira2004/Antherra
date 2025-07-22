using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    [SerializeField] private PlayerHealthData playerHealthData;
    public void Awake()
    {
        playerHealthData.ResetHealth();
    }
}