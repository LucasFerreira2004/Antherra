using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    [SerializeField] private PlayerHealthData playerHealthData;
    [SerializeField] private PlayerModifiedStatus playerModifiedStatus;
    public void Awake()
    {
        playerHealthData.ResetHealth();
        playerModifiedStatus.ResetValues();
    }
}