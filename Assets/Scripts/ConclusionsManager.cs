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
        DisplayMonsters(player.matches);
        DisplayMonsters(player.rejections);

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

    private void DisplayMonsters(List<Monster> monsters)
    {
        foreach (Monster m in monsters)
        {
            GameObject head = Instantiate(monsterHead, matchesContainer);
            Transform baseTransform = head.transform.Find(Constants.BASE_GAMEOBJECT_NAME);
            MonsterParts.CreatePortrait(m, head.transform, baseTransform, true);
            float yOffset = baseTransform.GetChild(0).transform.position.y;
            Debug.Log(yOffset);
            //head.transform.position = new Vector2(head.transform.position.x, head.transform.position.y - yOffset);
            Debug.Log(string.Format("{0}, {1}", m.name, m.age));
        }
    }

    private void SpotlightMonster(Monster monster)
    {
        spotlight.GetComponent<FillCard>().FillFullCard(monster, true);
    }
}
