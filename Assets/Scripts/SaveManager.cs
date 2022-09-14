using UnityEngine;
using System.Collections.Generic;
using static Traits;
using static GameConfig;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public static string playerName = "Quasimodo";
    public static string playerAge = "1000";
    public static List<int> playerTraits;

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
    }

    private void Start()
    {
        playerTraits = new List<int>();
    }

    /*
    // Initialize traits randomly
    public static void SelectRandomTraits()
    {
        // Select traits
        for (int i = 0; i < numOfTraits; i++)
        {
            // Only select from first half of traits list to avoid opposing traits
            int randomInt = Random.Range(0, traits.Count / 2);

            // Keep cycling if we have already picked that trait
            while (System.Array.Exists(playerTraits, x => x == randomInt))
                randomInt = Random.Range(0, traits.Count / 2);

            playerTraits[i] = randomInt;
        }

        // Random chance to choose an opposing trait instead
        for (int i = 0; i < numOfTraits; i++)
            playerTraits[i] += (traits.Count / 2) * Random.Range(0, 2);
    }*/
}