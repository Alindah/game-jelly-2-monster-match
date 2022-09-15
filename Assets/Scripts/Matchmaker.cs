using UnityEngine;
using static SaveManager;

public class Matchmaker : MonoBehaviour
{
    public void OnPressLike()
    {
        Debug.Log("You liked this monster!");
    }

    public void OnPressDislike()
    {
        Debug.Log("You rejected this monster :(");
    }
}
