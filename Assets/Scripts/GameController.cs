using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("GAMEPLAY")]
    public int numOfTraits;

    private void Awake()
    {
        GameConfig.numOfTraits = numOfTraits;
    }

    public static void MoveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
