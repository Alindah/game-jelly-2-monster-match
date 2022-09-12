using System;
using UnityEngine;
using static Traits;
using static GameConfig;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public static string playerName = "Quasimodo";
    public static string playerAge = "1000";
    public static int[] traitsIndex;

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

    // Initialize traits randomly
    public static void InitializeTraits()
    {
        traitsIndex = new int[numOfTraits];
        
        // Select traits
        for (int i = 0; i < numOfTraits; i++)
        {
            // Only select from first half of traits list to avoid opposing traits
            int randomInt = UnityEngine.Random.Range(0, traits.Count / 2);

            // Keep cycling if we have already picked that trait
            while (Array.Exists(traitsIndex, x => x == randomInt))
                randomInt = UnityEngine.Random.Range(0, traits.Count / 2);

            traitsIndex[i] = randomInt;
        }

        // Random chance to choose an opposing trait instead
        for (int i = 0; i < numOfTraits; i++)
            traitsIndex[i] += (traits.Count / 2) * UnityEngine.Random.Range(0, 2);
    }
}