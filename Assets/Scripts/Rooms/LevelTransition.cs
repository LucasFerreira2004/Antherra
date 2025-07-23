using UnityEngine;

public class LevelTransition : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        SceneLoader.LoadNextLevel();
    }
}
