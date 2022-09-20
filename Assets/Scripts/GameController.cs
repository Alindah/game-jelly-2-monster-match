using UnityEngine;
using UnityEngine.SceneManagement;
using static Constants;
using static SaveManager;

public class GameController : MonoBehaviour
{
    public static void MoveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void RestartGame()
    {
        SceneManager.LoadScene(CUSTOMIZATION_SCENE);
        InitializePlayer();
    }
}
