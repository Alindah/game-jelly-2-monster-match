using UnityEngine;
using TMPro;
using static Constants;

public class FillCard : MonoBehaviour
{
    public Transform portraitTransform;
    public Transform baseTransform;
    public TMP_Text portraitHeader;
    public TMP_Text portraitTraits;

    private string headerFormat = "";

    private void Awake()
    {
        headerFormat = portraitHeader.text;
    }

    public void FillFullCard(Monster monster)
    {
        FillInfo(monster);
        FillPortrait(monster);
    }

    public void ClearCard()
    {
        portraitHeader.text = headerFormat;
        portraitTraits.text = "";

        // Destroy all body parts except for base
        foreach (Transform child in portraitTransform)
        {
            if (child != baseTransform)
                Destroy(child.gameObject);
        }    
    }

    public void FillInfo(Monster monster, bool showCompatibility = false)
    {
        portraitHeader.text = string.Format(headerFormat, monster.name, monster.age);
        portraitTraits.text = "";

        foreach (int trait in monster.traits)
            portraitTraits.text += Traits.traits[trait] + "\n";

        if (showCompatibility)
        {
            TMP_Text compatibilityInfo = gameObject.transform.Find(COMPATIBILITY_INFO).GetComponent<TMP_Text>();
            compatibilityInfo.gameObject.SetActive(true);
            compatibilityInfo.text = string.Format(compatibilityInfo.text, monster.compatibility);
        }
    }

    public void FillPortrait(Monster monster)
    {
        for (int i = 0; i < monster.bodyParts.Length; i++)
        {
            // Do not instantiate a part if player chose none for it
            if (monster.bodyPartsInt[i] >= MonsterParts.partsList[i].Length)
                continue;

            monster.bodyParts[i] = Instantiate(MonsterParts.partsList[i][monster.bodyPartsInt[i]], portraitTransform);
            Debug.Log(monster.bodyParts[i]);
        }

        // Color bases
        if (monster.bodyPartsInt[MonsterParts.HEAD_INDEX] < MonsterParts.partsList[MonsterParts.HEAD_INDEX].Length)
            monster.bodyParts[MonsterParts.HEAD_INDEX].GetComponent<SpriteRenderer>().color = monster.baseColor;

        foreach (SpriteRenderer sprite in baseTransform.GetComponentsInChildren<SpriteRenderer>())
            sprite.color = monster.baseColor;
    }
}
