using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("GAMEPLAY")]
    public int numOfTraits;

    private void Start()
    {
        GameConfig.numOfTraits = numOfTraits;
    }
}
