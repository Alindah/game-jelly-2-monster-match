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
    public GameObject[] bodyParts;
    public int[] bodyPartsInt;

    // Monster constructor
    public Monster()
    {
        // Info
        name = "Lonely Monster";
        age = "100";
        traits = Traits.SelectRandomTraits();
        compatibility = 0;

        // Appearance
        baseColor = MonsterParts.RandomizeBaseColor();
        bodyParts = MonsterParts.RandomizeParts();
        bodyPartsInt = new int[MonsterParts.numOfPartsCategories];
    }
}