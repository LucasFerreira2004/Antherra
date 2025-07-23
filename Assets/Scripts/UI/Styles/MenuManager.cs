using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneLoader.LoadNewGame();
    }

    public void TryAgain()
    {
        SceneLoader.LoadNewGame();
    }
    public void MainMenu()
    {
        SceneLoader.LoadMainMenu();
    }

    public void Exit()
    {
        Application.Quit();
    }
}