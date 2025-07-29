using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private string nextLevelName;

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
        if (!collision.CompareTag("Player"))
            return;

        Debug.Log($"Iniciando transição para {nextLevelName}");
        TransitionManager.Instance.GoToTransition(nextLevelName);
    }
}