using UnityEngine;
using static SaveManager;

public class AppUIManager : MonoBehaviour
{
    public FillCard playerCard;
    public Transform playerPortraitTransform;
    public FillCard suitorCard;

    private void Start()
    {
        playerCard.FillInfo(player);
        playerCard.FillPortrait(player, playerPortraitTransform);
    }
}
