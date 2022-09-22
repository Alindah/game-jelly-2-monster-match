using System.Collections.Generic;
using UnityEngine;

public class Monster
{
    public string name;
    public string age;
    public List<int> traits = new List<int>();
    public float compatibility;

    // Appearance
    public Color baseColor;
    public GameObject eyes;
    public GameObject headDecor;
    public GameObject mouth;

    // Monster constructor
    public Monster()
    {
        name = "Lonely Monster";
        age = "100";
        traits = Traits.SelectRandomTraits();
        compatibility = 0;
    }
}