using UnityEngine;
using TMPro;
using static SaveManager;

public class ConclusionsManager : MonoBehaviour
{
    public TMP_Text matchesHeader;
    public TMP_Text rejectionsHeader;

    private void Start()
    {
        matchesHeader.text = string.Format(matchesHeader.text, matches.Count);
        rejectionsHeader.text = string.Format(rejectionsHeader.text, rejections.Count);
    }
}
