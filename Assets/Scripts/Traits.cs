using System.Collections.Generic;
using UnityEngine;
using static GameConfig;

public class Traits : MonoBehaviour
{
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
        "Silly"
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

    // Select traits randomly
    public static List<int> SelectRandomTraits()
    {
        List<int> traitsList = new List<int>();

        // Select traits
        for (int i = 0; i < numOfTraits; i++)
        {
            // Only select from first half of traits list to avoid opposing traits
            int randomInt = Random.Range(0, traits.Count / 2);

            // Keep cycling if we have already picked that trait
            while (traitsList.Contains(randomInt))
                randomInt = Random.Range(0, traits.Count / 2);

            traitsList.Add(randomInt);
        }

        // Random chance to choose an opposing trait instead
        for (int i = 0; i < numOfTraits; i++)
            traitsList[i] += (traits.Count / 2) * Random.Range(0, 2);

        return traitsList;
    }
}
