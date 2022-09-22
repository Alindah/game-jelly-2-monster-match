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

    public Transform[] transforms;
    public List<GameObject[]> partsList = new List<GameObject[]>();

    private List<string> partsFolderNames = new List<string>(Directory.GetDirectories(PARTS_PREFABS_PATH));

    private void Awake()
    {
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
    public GameObject[] InitializePartsPrefabs(string dirName)
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
    }
}
