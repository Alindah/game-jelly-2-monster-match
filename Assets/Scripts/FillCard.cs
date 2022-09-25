using UnityEngine;
using TMPro;
using static Constants;

public class FillCard : MonoBehaviour
{
    public Transform portraitTransform;
    public Transform baseTransform;
    public TMP_Text portraitHeader;
    public TMP_Text portraitTraits;

    private TMP_Text compatibilityInfo;
    private string headerFormat = "";
    private string compatibilityFormat = "";

    private void Awake()
    {
        headerFormat = portraitHeader.text;
        compatibilityInfo = gameObject.transform.Find(COMPATIBILITY_INFO).GetComponent<TMP_Text>();
        compatibilityFormat = compatibilityInfo.text;
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
            compatibilityInfo.gameObject.SetActive(true);
            compatibilityInfo.text = string.Format(compatibilityFormat, monster.compatibility);
        }
    }
}
