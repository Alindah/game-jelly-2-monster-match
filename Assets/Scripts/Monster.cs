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
        age = GenerateRandomAge(18, 65, 0.5f, 10.0f, 25);
        traits = Traits.SelectRandomTraits();
        compatibility = 0;

        // Appearance
        baseColor = MonsterParts.RandomizeBaseColor();
        bodyParts = new GameObject[SaveManager.monsterParts.numOfPartsCategories];
        bodyPartsInt = MonsterParts.RandomizeParts();
    }

    // Random age generator
    private string GenerateRandomAge(int minIn, int maxEx, float multiplierMin = 0.5f, float multiplierMax = 10, int multiplierChance = 10)
    {
        int age = Random.Range(minIn, maxEx);

        // Chance to multiply for an ancient monster
        if (Random.Range(0, 100) <= Random.Range(0, multiplierChance))
            age *= (int)Random.Range(multiplierMin, multiplierMax);

        return age.ToString();
    }
}