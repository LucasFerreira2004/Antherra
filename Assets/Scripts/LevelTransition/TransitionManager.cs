using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    public string nextLevelName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Permite persistir entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoToTransition(string nextLevel)
    {
        nextLevelName = nextLevel;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelTransition");
    }

    public void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelName);
        }
    }
}
