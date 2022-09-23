using UnityEngine;
using static SaveManager;

public class AppUIManager : MonoBehaviour
{
    public FillCard playerCard;
    public FillCard suitorCard;

    public Matchmaker matchmaker;

    private void Start()
    {
        playerCard.FillFullCard(player);
        suitorCard.FillFullCard(matchmaker.suitor);
    }

    public void ShowNextSuitor()
    {
        suitorCard.ClearCard();
        suitorCard.FillFullCard(matchmaker.suitor);
    }
}
