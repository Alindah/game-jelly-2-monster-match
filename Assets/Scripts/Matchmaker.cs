using UnityEngine;
using static SaveManager;
using static Constants;

public class Matchmaker : MonoBehaviour
{
    public int baseMatchChance = 50;
    public int likeIncrease = 15;
    public int dislikeDecrease = 15;
    public int deckSize = 30;
    public int swipesAvailable = 10;

    public Monster suitor;
    public AppUIManager appUIManager;

    public void Awake()
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
        GenerateNewSuitor();
        deckSize--;
        EndConditions();
    }

    private int CalculateMatchChance()
    {
        int matchChance = baseMatchChance;

        foreach (int traitIndex in suitor.traits)
        {
            if (player.traits.Contains(traitIndex))
                matchChance += likeIncrease;
            else if (player.opposingTraits.Contains(traitIndex))
                matchChance -= dislikeDecrease;
        }

        return matchChance;
    }

    private void DetermineMatch()
    {
        int chemistry = Random.Range(0, 101);
        suitor.compatibility = CalculateMatchChance();

        if (chemistry <= suitor.compatibility)
            player.matches.Add(suitor);
        else
            player.rejections.Add(suitor);
    }

    private void EndConditions()
    {
        if (swipesAvailable <= 0 || deckSize <= 0)
        {
            GameController.MoveToScene(CONCLUSION_SCENE);
        }
        else
        {
            appUIManager.UpdateUIText();
            appUIManager.ShowNextSuitor();
        }
    }
}
