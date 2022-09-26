using UnityEngine;
using TMPro;
using static SaveManager;

public class AppUIManager : MonoBehaviour
{
    public FillCard playerCard;
    public FillCard suitorCard;
    public TMP_Text likesText;
    public TMP_Text swipesText;

    public Matchmaker matchmaker;

    private string likesFormat;
    private string swipesFormat;

    private void Start()
    {
        likesFormat = likesText.text;
        swipesFormat = swipesText.text;
        playerCard.FillFullCard(player);
        suitorCard.FillFullCard(matchmaker.suitor);
        UpdateUIText();
    }

    public void ShowNextSuitor()
    {
        suitorCard.ClearCard();
        suitorCard.FillFullCard(matchmaker.suitor);
    }

    public void UpdateUIText()
    {
        likesText.text = string.Format(likesFormat, matchmaker.swipesAvailable.ToString());
        swipesText.text = string.Format(swipesFormat, matchmaker.deckSize.ToString());
    }
}
