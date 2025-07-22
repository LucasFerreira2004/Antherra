using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadNextLevel()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }

    public static void ReloadCurrentScene()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }


    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("chamou");
        // Busca o ponto de spawn na cena nova
        GameObject spawnPoint = GameObject.FindWithTag("PlayerSpawnPoint");
        if (spawnPoint != null && PlayerStatus.Instance != null)
        {
            Debug.Log("entrou");
            PlayerStatus.Instance.gameObject.transform.position = spawnPoint.transform.position;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
