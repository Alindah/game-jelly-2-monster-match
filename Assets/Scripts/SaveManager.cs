using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public static Player player;
    public static MonsterParts monsterParts;

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

    public static void InitializePlayer()
    {
        player = new Player();
    }
}