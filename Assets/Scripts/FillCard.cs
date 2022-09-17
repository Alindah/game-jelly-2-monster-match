using UnityEngine;
using TMPro;
using static Constants;

public class FillCard : MonoBehaviour
{
    public static void FillInfo(Monster monster, bool showCompatibility = false)
    {
        TMP_Text portraitHeader = GameObject.Find(MONSTER_INFO).GetComponent<TMP_Text>();
        TMP_Text portraitTraits = GameObject.Find(TRAITS_INFO).GetComponent<TMP_Text>();
        TMP_Text compatibilityInfo = GameObject.Find(COMPATIBILITY_INFO).GetComponent<TMP_Text>();

        portraitHeader.text = string.Format(portraitHeader.text, monster.name, monster.age);
        portraitTraits.text = "";
        portraitHeader.text = string.Format(portraitHeader.text, monster.compatibility);

        foreach (int trait in monster.traits)
            portraitTraits.text += Traits.traits[trait] + "\n";

        if (showCompatibility)
            compatibilityInfo.gameObject.SetActive(true);
    }
}
