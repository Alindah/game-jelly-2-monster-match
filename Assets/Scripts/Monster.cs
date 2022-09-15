using System.Collections.Generic;

public class Monster
{
    public string name;
    public string age;
    public List<int> traits = new List<int>();

    // Monster constructor
    public Monster()
    {
        name = "Lonely Monster";
        age = "100";
        traits = Traits.SelectRandomTraits();
    }
}