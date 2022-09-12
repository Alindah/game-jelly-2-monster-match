using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public static string playerName = "Quasimodo";
    public static string playerAge = "1000";
    public static int[] traitsIndex = { 0, 1, 2 };

    private void Awake()
    {
        // Prevent multiple instances of SaveManager (singleton pattern)
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Prevent this game object from being destroyed upon restart
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeTraits();
    }

    private void Start()
    {
        
    }

    // Initialize traits randomly
    private void InitializeTraits()
    {
        traitsIndex = new int[GameConfig.numOfTraits];

        for (int i = 0; i < GameConfig.numOfTraits; i++)
        {
            // Only select from first half of traits list to avoid opposing traits
            int randomInt = Random.Range(0, GameConfig.numOfTraits / 2);

            // Keep cycling if we have already picked that trait
            while (traitsIndex.Contains(randomInt))
                randomInt = Random.Range(0, GameConfig.numOfTraits / 2);

            // Random chance to choose an opposing trait instead
            traitsIndex[i] = randomInt + GameConfig.numOfTraits / 2 * Random.Range(0, 2);
        }
    }

}