using UnityEngine;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public static string playerName = "Quasimodo";
    public static string playerAge = "1000";
    public static List<int> playerTraits;
    public static List<int> opposingTraits;
    public static List<Monster> matches;
    public static List<Monster> rejections;

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

        // Create lists
        playerTraits = new List<int>();
        opposingTraits = new List<int>();
    }
}