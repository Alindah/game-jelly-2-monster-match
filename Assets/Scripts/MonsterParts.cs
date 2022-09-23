using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine;
using static Constants;

public class MonsterParts : MonoBehaviour
{
    public static string[] partsDir = Directory.GetDirectories(PARTS_PATH);
    public static int numOfPartsCategories = partsDir.Length;
    public static string[] partsCategoryNames = new string[numOfPartsCategories];
    public static List<GameObject[]> partsList = new List<GameObject[]>();
    public static List<string> partsFolderNames = new List<string>(Directory.GetDirectories(PARTS_PREFABS_PATH));
    public static int HEAD_INDEX = 1;   // This is the category part that is the head (MAY CHANGE BASED ON FOLDER ORDER)

    public Transform[] transforms;

    public static void InitializeMonsterParts()
    {
        // Initial parts data
        for (int i = 0; i < numOfPartsCategories; i++)
        {
            // Get category labels
            Regex regex = new Regex(PARTS_PATH + '/');
            partsCategoryNames[i] = regex.Replace(partsDir[i], "");

            // Get category folder names
            regex = new Regex(Path.GetDirectoryName(partsFolderNames[i]) + '/');
            partsFolderNames[i] = PARTS_PREFABS_PATH_SHORT + regex.Replace(partsFolderNames[i], "");

            // Add prefabs to each category
            partsList.Add(InitializePartsPrefabs(partsFolderNames[i]));
        }
    }

    // Fill parts prefab arrays
    public static GameObject[] InitializePartsPrefabs(string dirName)
    {
        return Resources.LoadAll<GameObject>(dirName);
    }

    // Set body part of a monster
    public void SetBodyPart(int categoryIndex, int partIndex, Monster monster)
    {
        if (monster.bodyParts[categoryIndex] != null)
            Destroy(monster.bodyParts[categoryIndex]);

        if (partIndex >= partsList[categoryIndex].Length)
            return;

        monster.bodyParts[categoryIndex] = Instantiate(partsList[categoryIndex][partIndex], transforms[categoryIndex]);
        monster.bodyPartsInt[categoryIndex] = partIndex;
    }

    // Randomize body parts
    public static GameObject[] RandomizeParts()
    {
        GameObject[] parts = new GameObject[numOfPartsCategories];

        for (int i = 0; i < numOfPartsCategories; i++)
            parts[i] = partsList[i][Random.Range(0, partsList[i].Length)];

        return parts;
    }

    // Randomize base color
    public static Color RandomizeBaseColor()
    {
        float randomRed = Random.Range(0, 1.0f);
        float randomGreen = Random.Range(0, 1.0f);
        float randomBlue = Random.Range(0, 1.0f);

        return new Color(randomRed, randomGreen, randomBlue, 1);
    }
}
