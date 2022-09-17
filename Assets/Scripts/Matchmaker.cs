using UnityEngine;
using static SaveManager;
using static Traits;
using static Constants;

public class Matchmaker : MonoBehaviour
{
    public int baseMatchChance = 50;
    public int likeIncrease = 15;
    public int dislikeDecrease = 15;
    public int deckSize = 30;
    public int swipesAvailable = 10;

    private Monster suitor;

    public void Start()
    {
        GenerateNewSuitor();
    }

    public void GenerateNewSuitor()
    {
        suitor = new Monster();
    }

    public void OnPressLike()
    {
        DetermineMatch();
        GenerateNewSuitor();
        swipesAvailable--;
        deckSize--;
        EndConditions();
    }

    public void OnPressDislike()
    {
        Debug.Log("You rejected this monster :(");
        GenerateNewSuitor();
        deckSize--;
        EndConditions();
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
        suitor.compatibility = CalculateMatchChance();

        if (chemistry <= suitor.compatibility)
        {
            Debug.Log("congrats, you matched!");
            matches.Add(suitor);
        }
        else
        {
            Debug.Log("they rejected your ugly ass");
            rejections.Add(suitor);
        }
    }

    private void EndConditions()
    {
        if (swipesAvailable <= 0 || deckSize <= 0)
            GameController.MoveToScene(CONCLUSION_SCENE);
    }
}
