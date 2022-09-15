using UnityEngine;
using static SaveManager;
using static Traits;

public class Matchmaker : MonoBehaviour
{
    public float baseMatchChance = 50;
    public float likeIncrease = 15;
    public float dislikeDecrease = 15;
    private Monster suitor;

    public void Start()
    {
        GenerateNewSuitor();
    }

    public void GenerateNewSuitor()
    {
        suitor = new Monster();

        Debug.Log(suitor.name + ", " + suitor.age);
        foreach (int i in suitor.traits)
        {
            Debug.Log(traits[i]);
        }

        CalculateMatchChance();
    }

    public void OnPressLike()
    {
        Debug.Log("You liked this monster!");
        GenerateNewSuitor();
    }

    public void OnPressDislike()
    {
        Debug.Log("You rejected this monster :(");
        GenerateNewSuitor();
    }

    public void CalculateMatchChance()
    {
        float matchChance = baseMatchChance;

        foreach (int traitIndex in suitor.traits)
        {
            if (playerTraits.Contains(traitIndex))
                matchChance += likeIncrease;
            else if (opposingTraits.Contains(traitIndex))
                matchChance -= dislikeDecrease;
        }

        Debug.Log("you have a " + matchChance + "% chance of matching");
    }
}
