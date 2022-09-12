using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("GAMEPLAY")]
    public int numOfTraits;

    private void Awake()
    {
        GameConfig.numOfTraits = numOfTraits;
    }
}
