using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    public string nextLevelName;
    public bool useSpecialTransition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Garante que não será destruído em nenhuma circunstância
            transform.SetParent(null); // Remove qualquer parent que possa causar destruição
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void GoToTransition(string nextLevel)
    {
        // Força a persistência mesmo se chamado múltiplas vezes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        nextLevelName = nextLevel;
        useSpecialTransition = (SceneManager.GetActiveScene().name == "Level2" && nextLevel == "Level3");
        
        string transitionScene = useSpecialTransition ? "LevelTransitionSpecial" : "LevelTransition";
        SceneManager.LoadScene(transitionScene);
    }

    public void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            Debug.Log($"Carregando nível: {nextLevelName}");
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogError("Nome do próximo nível não definido!");
        }
    }
}