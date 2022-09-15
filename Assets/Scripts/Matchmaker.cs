using UnityEngine;
using static SaveManager;
using static Traits;

public class Matchmaker : MonoBehaviour
{
    public int baseMatchChance = 50;
    public int likeIncrease = 15;
    public int dislikeDecrease = 15;

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
        DetermineMatch();
        GenerateNewSuitor();
    }

    public void OnPressDislike()
    {
        Debug.Log("You rejected this monster :(");
        GenerateNewSuitor();
    }

    private int CalculateMatchChance()
    {
        int matchChance = baseMatchChance;

        foreach (int traitIndex in suitor.traits)
        {
            if (playerTraits.Contains(traitIndex))
                matchChance += likeIncrease;
            else if (opposingTraits.Contains(traitIndex))
                matchChance -= dislikeDecrease;
        }

        Debug.Log("you have a " + matchChance + "% chance of matching");
        return matchChance;
    }

    private void DetermineMatch()
    {
        int chemistry = Random.Range(0, 101);

        if (chemistry >= CalculateMatchChance())
        {
            Debug.Log("congrats, you matched!");
            suitor.match = true;
            matches.Add(suitor);
        }
        else
        {
            Debug.Log("they rejected your ugly ass");
            rejections.Add(suitor);
        }
    }
}
