using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour
{
    [SerializeField] private PlayerHealthData playerHealthData;
    [SerializeField] private PlayerModifiedStatus playerModifiedStatus;
    public void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2) return;

        playerHealthData.ResetHealth();
        playerModifiedStatus.ResetValues();
    }
}