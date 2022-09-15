using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static void MoveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
