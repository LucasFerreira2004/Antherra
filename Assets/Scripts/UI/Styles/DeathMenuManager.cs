using UnityEngine;

public class DeathMenuManager : MonoBehaviour
{
    public void TryAgain()
    {
        SceneLoader.LoadNewGame();
    }
    public void MainMenu()
    {
        SceneLoader.LoadMainMenu();
    }
}