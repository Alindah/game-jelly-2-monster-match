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

    public void FillFullCard(Monster monster, bool showCompatibility = false)
    {
        FillInfo(monster, showCompatibility);
        MonsterParts.CreatePortrait(monster, portraitTransform, baseTransform);
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
}
