using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static SaveManager;

public class ConclusionsManager : MonoBehaviour
{
    public TMP_Text matchesHeader;
    public TMP_Text rejectionsHeader;

    [Header("Spotlight")]
    public GameObject spotlight;

    private List<Monster> allMonsters;

    private void Start()
    {
        // Create a list of all monsters, both matched and rejected
        allMonsters = new List<Monster>(player.matches);
        allMonsters.InsertRange(player.matches.Count, player.rejections);

        // Display number of matches and rejections
        matchesHeader.text = string.Format(matchesHeader.text, player.matches.Count);
        rejectionsHeader.text = string.Format(rejectionsHeader.text, player.rejections.Count);

        // Display monster cards
        DisplayMonsters(player.matches);
        DisplayMonsters(player.rejections);

        // If player rejected all monsters, display player in  spotlight
        if (player.matches.Count == 0 && player.rejections.Count == 0)
            SpotlightMonster(player);

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
        spotlight.GetComponent<FillCard>().FillInfo(monster, true);
    }
}
