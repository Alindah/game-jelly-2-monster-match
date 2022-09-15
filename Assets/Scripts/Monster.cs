using System.Collections.Generic;

public class Monster
{
    public string name;
    public string age;
    public List<int> traits = new List<int>();
    public bool match;

    // Monster constructor
    public Monster()
    {
        name = "Lonely Monster";
        age = "100";
        traits = Traits.SelectRandomTraits();
        match = false;
    }
}