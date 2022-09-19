using UnityEngine;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public static Player player;

    private const string DEFAULT_PLAYER_NAME = "Quasimodo";
    private const string DEFAULT_PLAYER_AGE = "1000";

    public class Player : Monster
    {
        public List<int> opposingTraits;
        public List<Monster> matches;
        public List<Monster> rejections;

        public Player()
        {
            name = DEFAULT_PLAYER_NAME;
            age = DEFAULT_PLAYER_AGE;
            traits = new List<int>();
            opposingTraits = new List<int>();
            matches = new List<Monster>();
            rejections = new List<Monster>();
        }
    }

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

        InitializePlayer();
    }

    private void InitializePlayer()
    {
        player = new Player();
    }
}