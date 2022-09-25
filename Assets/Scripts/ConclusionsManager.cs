using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static SaveManager;

public class ConclusionsManager : MonoBehaviour
{
    public TMP_Text matchesHeader;
    public TMP_Text rejectionsHeader;
    public GameObject trueLoveGameObj;
    public GameObject monsterHead;
    public Transform matchesContainer;
    public Transform matchesContainer2;
    public Transform rejectionsContainer;
    public Transform rejectionsContainer2;
    public float xOffset = 2f;
    public int maxPerRow = 5;

    [Header("Spotlight")]
    public GameObject spotlight;
    public Transform spotlightTransform;

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
        DisplayMonsters(player.matches, matchesContainer, matchesContainer2);
        DisplayMonsters(player.rejections, rejectionsContainer, rejectionsContainer2);

        // If player rejected all monsters, display player in  spotlight
        // otherwise, spotlight first monster
        if (player.matches.Count == 0 && player.rejections.Count == 0)
        {
            SpotlightMonster(player);
            trueLoveGameObj.SetActive(true);
        }
        else
        {
            SpotlightMonster(allMonsters[0]);
        }
    }

    private void DisplayMonsters(List<Monster> monsters, Transform container, Transform container2)
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            Transform tf = i < maxPerRow ? container : container2;
            GameObject head = Instantiate(monsterHead, tf);
            Transform baseTransform = head.transform.Find(Constants.BASE_GAMEOBJECT_NAME);
            MonsterParts.CreatePortrait(monsters[i], head.transform, baseTransform, true);

            // Y Offset
            float yOffset = baseTransform.GetChild(0).transform.localPosition.y / 2;

            head.transform.position = new Vector2(head.transform.position.x + xOffset * (i % maxPerRow), head.transform.position.y - yOffset);
        }
    }

    private void SpotlightMonster(Monster monster)
    {
        spotlight.GetComponent<FillCard>().FillFullCard(monster, true);
    }
}
