using UnityEngine;

public class CutsceneEvents : MonoBehaviour
{
    public void LoadNextLevel()
    {
        Debug.Log("Cutscene terminada - Carregando próximo nível");
        
        // Usa o TransitionManager para carregar o próximo nível
        if (TransitionManager.Instance != null)
        {
            TransitionManager.Instance.LoadNextLevel();
        }
        else
        {
            // Debug.LogError("TransitionManager.Instance está nulo!");
            // Fallback: carrega direto se o TransitionManager falhar
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
        }
    }
}