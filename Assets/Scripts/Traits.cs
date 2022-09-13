using System.Collections.Generic;
using UnityEngine;

public class Traits : MonoBehaviour
{
    // Need to be List<string> instead of array to add to TMP_Dropdown type as options
    public static List<string> traits = new List<string>
    {
        // Traits
        "Sleepy",
        "Sad",
        "Angry",
        "Slob",
        "Aloof",
        "Evil",
        "Serious",

        // Opposing traits - order must correspond with above traits
        "Energetic",
        "Happy",
        "Calm",
        "Neat",
        "Romantic",
        "Good",
        "Silly",
    };

    // Return opposite trait as a string
    public static string GetOppositeTrait(int traitIndex)
    {
        return traits[GetOppositeTraitIndex(traitIndex)];
    }

    // Return opposite trait as an index
    public static int GetOppositeTraitIndex(int traitIndex)
    {
        int opposingTraitIndex = traitIndex < traits.Count / 2 ? traitIndex + traits.Count / 2 : traitIndex - traits.Count / 2;
        return opposingTraitIndex;
    }
}
