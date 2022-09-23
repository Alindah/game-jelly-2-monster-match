using System.Collections.Generic;

public class Player : Monster
{
    public List<int> opposingTraits;
    public List<Monster> matches;
    public List<Monster> rejections;

    public Player()
    {
        traits = new List<int>();
        compatibility = 100;
        opposingTraits = new List<int>();
        matches = new List<Monster>();
        rejections = new List<Monster>();
    }
}