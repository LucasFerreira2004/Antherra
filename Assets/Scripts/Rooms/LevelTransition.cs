using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    public bool useSpecialTransition;

    void OnValidate()
    {
        // Garante que o nome do nível está correto no Inspector
        if (string.IsNullOrEmpty(nextLevelName))
        {
            Debug.LogWarning("Próximo nível não definido!", this);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        // Verifica automaticamente se é o Level3
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            SceneManager.LoadScene("Win");
            return;
        }

        // Comportamento normal para outros níveis
        TransitionManager.Instance.GoToTransition(nextLevelName);
    }
}