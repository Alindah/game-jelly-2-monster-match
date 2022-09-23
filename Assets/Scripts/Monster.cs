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
        name = NamesList.GenerateRandomName();
        age = Random.Range(0, 1001).ToString();
        traits = Traits.SelectRandomTraits();
        compatibility = 0;

        // Appearance
        baseColor = MonsterParts.RandomizeBaseColor();
        bodyParts = new GameObject[MonsterParts.numOfPartsCategories];
        bodyPartsInt = MonsterParts.RandomizeParts();
    }
}