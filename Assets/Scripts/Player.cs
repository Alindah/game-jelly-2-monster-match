using System.Collections.Generic;

public class Player : Monster
{
    public List<int> opposingTraits;
    public List<Monster> matches;
    public List<Monster> rejections;

    private const string DEFAULT_PLAYER_NAME = "Quasimodo";
    private const string DEFAULT_PLAYER_AGE = "1000";

    public Player()
    {
        name = DEFAULT_PLAYER_NAME;
        age = DEFAULT_PLAYER_AGE;
        traits = new List<int>();
        compatibility = 100;
        opposingTraits = new List<int>();
        matches = new List<Monster>();
        rejections = new List<Monster>();
    }
}