using UnityEngine;
using TMPro;
using static Constants;

public class FillCard : MonoBehaviour
{
    public Transform baseTransform;
    private SpriteRenderer[] baseParts;

    private void Start()
    {
        //baseParts = baseTransform.GetComponentsInChildren<SpriteRenderer>();

    }

    public void FillInfo(Monster monster, bool showCompatibility = false)
    {
        TMP_Text portraitHeader = GameObject.Find(MONSTER_INFO).GetComponent<TMP_Text>();
        TMP_Text portraitTraits = GameObject.Find(TRAITS_INFO).GetComponent<TMP_Text>();

        portraitHeader.text = string.Format(portraitHeader.text, monster.name, monster.age);
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

    public void FillPortrait(Monster monster, Transform transform)
    {
        for (int i = 0; i < monster.bodyParts.Length; i++)
            monster.bodyParts[i] = Instantiate(MonsterParts.partsList[i][monster.bodyPartsInt[i]], transform);

        // Color bases
        monster.bodyParts[MonsterParts.HEAD_INDEX].GetComponent<SpriteRenderer>().color = monster.baseColor;

        foreach (SpriteRenderer sprite in baseTransform.GetComponentsInChildren<SpriteRenderer>())
            sprite.color = monster.baseColor;
    }
}
