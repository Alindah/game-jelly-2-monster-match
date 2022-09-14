using System.Collections.Generic;
using UnityEngine;

public class Traits : MonoBehaviour
{
    public static List<(int, string)> traits = new List<(int, string)>
    {
        // Traits
        (0, "Sleepy"),
        (1, "Sad"),
        (2, "Angry"),
        (3, "Slob"),
        (4, "Aloof"),
        (5, "Evil"),
        (6, "Serious"),

        // Opposing traits - order must correspond with above traits
        (7, "Energetic"),
        (8, "Happy"),
        (9, "Calm"),
        (10, "Neat"),
        (11, "Romantic"),
        (12, "Good"),
        (13, "Silly")
    };

    // Return opposite trait as a string
    public static string GetOppositeTrait(int traitIndex)
    {
        return traits[GetOppositeTraitIndex(traitIndex)].Item2;
    }

    // Return opposite trait as an index
    public static int GetOppositeTraitIndex(int traitIndex)
    {
        int opposingTraitIndex = traitIndex < traits.Count / 2 ? traitIndex + traits.Count / 2 : traitIndex - traits.Count / 2;
        return opposingTraitIndex;
    }
}
