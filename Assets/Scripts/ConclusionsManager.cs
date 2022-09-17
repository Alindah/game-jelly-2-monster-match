using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static SaveManager;

public class ConclusionsManager : MonoBehaviour
{
    public TMP_Text matchesHeader;
    public TMP_Text rejectionsHeader;
    public TMP_Text compatibilityHeader;

    [Header("Spotlight")]
    public TMP_Text infoHeader;
    public TMP_Text traitsList;

    private List<Monster> allMonsters;

    private void Start()
    {
        // Create a list of all monsters, both matched and rejected
        allMonsters = matches;
        allMonsters.InsertRange(matches.Count, rejections);

        // Display number of matches and rejections
        matchesHeader.text = string.Format(matchesHeader.text, matches.Count);
        rejectionsHeader.text = string.Format(rejectionsHeader.text, rejections.Count);

        // Display monster cards
        DisplayMonsters(matches);
        DisplayMonsters(rejections);

        // If player rejected all monsters, display player in  spotlight
        //if (matches.Count == 0 && rejections.Count == 0)
        //    SpotlightMonster(player)

        // Spotlight first monster
        SpotlightMonster(allMonsters[0]);
    }

    private void DisplayMonsters(List<Monster> monsters)
    {
        Debug.Log("**************");
        foreach (Monster m in monsters)
        {
            Debug.Log(string.Format("{0}, {1}", m.name, m.age));
        }
    }

    private void SpotlightMonster(Monster monster)
    {
    }
}
