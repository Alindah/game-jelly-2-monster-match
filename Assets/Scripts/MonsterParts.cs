using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine;
using static Constants;

public class MonsterParts : MonoBehaviour
{
    public string[] partsDir;
    public int numOfPartsCategories;
    public string[] partsCategoryNames;
    public List<GameObject[]> partsList;
    public List<string> partsFolderNames;
    public int HEAD_INDEX = 1;   // This is the category part that is the head (MAY CHANGE BASED ON FOLDER ORDER)

    public Transform[] transforms;

    private void Awake()
    {
        partsDir = Directory.GetDirectories(PARTS_PATH);
        numOfPartsCategories = partsDir.Length;
        partsCategoryNames = new string[numOfPartsCategories];
        partsList = new List<GameObject[]>();
        partsFolderNames = new List<string>(Directory.GetDirectories(PARTS_PREFABS_PATH));
        SaveManager.monsterParts = this;
    }

    public static void InitializeMonsterParts()
    {
        // Initial parts data
        for (int i = 0; i < SaveManager.monsterParts.numOfPartsCategories; i++)
        {
            // Get category labels
            Regex regex = new Regex(PARTS_PATH + '/');
            SaveManager.monsterParts.partsCategoryNames[i] = regex.Replace(SaveManager.monsterParts.partsDir[i], "");

            // Get category folder names
            regex = new Regex(Path.GetDirectoryName(SaveManager.monsterParts.partsFolderNames[i]) + '/');
            SaveManager.monsterParts.partsFolderNames[i] = PARTS_PREFABS_PATH_SHORT + regex.Replace(SaveManager.monsterParts.partsFolderNames[i], "");

            // Add prefabs to each category
            SaveManager.monsterParts.partsList.Add(InitializePartsPrefabs(SaveManager.monsterParts.partsFolderNames[i]));
        }
    }

    // Fill parts prefab arrays
    public static GameObject[] InitializePartsPrefabs(string dirName)
    {
        return Resources.LoadAll<GameObject>(dirName);
    }

    public void SetBodyPart(int categoryIndex, int partIndex, Monster monster)
    {
        Transform tf = transforms[categoryIndex];

        if (monster.bodyParts[categoryIndex] != null)
            Destroy(monster.bodyParts[categoryIndex]);

        if (partIndex >= partsList[categoryIndex].Length)
        {
            monster.bodyPartsInt[categoryIndex] = partIndex;
            return;
        }

        monster.bodyParts[categoryIndex] = Instantiate(partsList[categoryIndex][partIndex], tf);
        monster.bodyPartsInt[categoryIndex] = partIndex;
    }

    // Randomize body parts
    public static int[] RandomizeParts()
    {
        int[] partsInt = new int[SaveManager.monsterParts.numOfPartsCategories];

        for (int i = 0; i < SaveManager.monsterParts.numOfPartsCategories; i++)
            partsInt[i] = Random.Range(0, SaveManager.monsterParts.partsList[i].Length);

        return partsInt;
    }

    // Randomize base color
    public static Color RandomizeBaseColor()
    {
        float randomRed = Random.Range(0, 1.0f);
        float randomGreen = Random.Range(0, 1.0f);
        float randomBlue = Random.Range(0, 1.0f);

        return new Color(randomRed, randomGreen, randomBlue, 1);
    }

    public static void CreatePortrait(Monster monster, Transform mainTransform, Transform baseTransform, bool moveHead = false)
    {
        for (int i = 0; i < monster.bodyParts.Length; i++)
        {
            // Do not instantiate a part if player chose none for it
            if (monster.bodyPartsInt[i] >= SaveManager.monsterParts.partsList[i].Length)
                continue;

            monster.bodyParts[i] = Instantiate(SaveManager.monsterParts.partsList[i][monster.bodyPartsInt[i]], mainTransform);
        }

        // Deal with head
        if (monster.bodyPartsInt[SaveManager.monsterParts.HEAD_INDEX] < SaveManager.monsterParts.partsList[SaveManager.monsterParts.HEAD_INDEX].Length)
        {
            if (moveHead)
                monster.bodyParts[SaveManager.monsterParts.HEAD_INDEX].transform.parent = baseTransform;
            else
                monster.bodyParts[SaveManager.monsterParts.HEAD_INDEX].GetComponent<SpriteRenderer>().color = monster.baseColor;
        }

        // Color base
        foreach (SpriteRenderer sprite in baseTransform.GetComponentsInChildren<SpriteRenderer>())
            sprite.color = monster.baseColor;
    }
}
