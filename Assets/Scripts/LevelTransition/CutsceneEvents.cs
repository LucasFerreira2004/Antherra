using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneEvents : MonoBehaviour
{
    public string nextLevelName; // Pode remover esta linha se não for usar fallback
    public bool useSpecialTransition;

    void Start()
    {
        // Verificação de debug
        if (TransitionManager.Instance == null)
        {
            Debug.LogError("TransitionManager não encontrado! Criando novo...");
            var go = new GameObject("TempTransitionManager");
            go.AddComponent<TransitionManager>();
        }
    }

    public void LoadNextLevel()
    {
        Debug.Log("Tentando carregar próximo nível...");
        
        if (TransitionManager.Instance != null)
        {
            Debug.Log($"Usando TransitionManager. Próximo nível: {TransitionManager.Instance.nextLevelName}");
            TransitionManager.Instance.LoadNextLevel();
        }
        else
        {
            Debug.LogError("TransitionManager não disponível! Usando fallback...");
            
            // Fallback emergencial - só para debug
            if (!string.IsNullOrEmpty(nextLevelName))
            {
                SceneManager.LoadScene(nextLevelName);
            }
            else
            {
                // Último recurso - tenta carregar Level2
                SceneManager.LoadScene("Level2");
            }
        }
    }
}