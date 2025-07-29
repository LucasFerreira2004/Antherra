using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector timeline;
    public string nextSceneName = "MainMenu";
    public Button skipButton;
    
    private bool hasEnded = false;

    void Start()
    {
        // Garante que temos uma timeline para trabalhar
        if (timeline == null)
        {
            timeline = GetComponent<PlayableDirector>();
            if (timeline == null)
            {
                Debug.LogError("Nenhum PlayableDirector encontrado!");
                return;
            }
        }

        // Configuração importante para o PlayableDirector
        timeline.extrapolationMode = DirectorWrapMode.Hold;
        timeline.stopped += OnCutsceneEnd;
        
        // Configura o botão de pular
        if (skipButton != null)
        {
            skipButton.onClick.AddListener(SkipCutscene);
        }

        // Inicia verificação periódica como fallback
        InvokeRepeating("CheckTimelineCompletion", 1f, 0.5f);
    }

    void Update()
    {
        // Verificação adicional por segurança
        if (!hasEnded && timeline.time >= timeline.duration - 0.1f)
        {
            OnCutsceneEnd(timeline);
        }
    }

    void CheckTimelineCompletion()
    {
        // Fallback para caso o evento stopped não seja chamado
        if (!hasEnded && timeline.state != PlayState.Playing)
        {
            OnCutsceneEnd(timeline);
        }
    }

    public void SkipCutscene()
    {
        if (hasEnded) return;
        
        Debug.Log("Cutscene pulada pelo jogador");
        timeline.Stop(); // Isso deve disparar o evento stopped
    }

    void OnCutsceneEnd(PlayableDirector director)
    {
        if (hasEnded) return;
        hasEnded = true;
        
        Debug.Log("Cutscene finalizada - Carregando: " + nextSceneName);
        
        // Para todas as verificações
        CancelInvoke("CheckTimelineCompletion");
        
        // Carrega a cena do menu
        LoadMenu();
    }

    void LoadMenu()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("Nome da próxima cena não definido!");
            return;
        }

        if (SceneManager.GetActiveScene().name != nextSceneName)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void OnDestroy()
    {
        // Limpeza segura
        if (timeline != null)
        {
            timeline.stopped -= OnCutsceneEnd;
        }
        
        if (skipButton != null)
        {
            skipButton.onClick.RemoveListener(SkipCutscene);
        }
        
        CancelInvoke("CheckTimelineCompletion");
    }
}