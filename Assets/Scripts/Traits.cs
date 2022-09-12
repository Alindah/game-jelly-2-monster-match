using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Traits : MonoBehaviour
{
    public TMP_Dropdown[] traitDropdowns;

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
        PopulateTraits();
    }

    // Populate dropdown with traits
    public void PopulateTraits()
    {
        foreach (TMP_Dropdown dd in traitDropdowns)
        {
            dd.AddOptions(traits);
        }
    }
}
