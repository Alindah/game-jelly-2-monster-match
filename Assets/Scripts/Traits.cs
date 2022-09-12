using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Traits : MonoBehaviour
{
    public GameObject traitDropdownObj;
    public Transform traitsTransform;
    public float traitsDropdownSpacing = -34; // Spacing between traits dropdown boxes

    private TMP_Dropdown[] traitDropdowns;

    private List<string> traits = new List<string>
    {
        "Sleepy",
        "Sad",
        "Angry",
        "Slob",
        "Aloof",
        "Evil",
        "Serious",

        "Energetic",
        "Happy",
        "Calm",
        "Neat",
        "Romantic",
        "Good",
        "Silly",
    };

    private void Start()
    {
        PopulateDropdown();
    }

    // Populate dropdown with traits
    public void PopulateDropdown()
    {
        traitDropdowns = new TMP_Dropdown[3];

        // Create new dropdown objects depending on number of traits indicated
        for (int i = 0; i < GameConfig.numOfTraits; i++)
        {
            GameObject obj = Instantiate(traitDropdownObj,
                new Vector2(traitsTransform.position.x, traitsTransform.position.y + traitsDropdownSpacing * i),
                Quaternion.identity,
                traitsTransform);

            traitDropdowns[i] = obj.GetComponent<TMP_Dropdown>();
        }

        foreach (TMP_Dropdown dd in traitDropdowns)
        {
            dd.AddOptions(traits);
        }
    }
}
