using UnityEngine;
using static Constants;

public class MonsterHead : MonoBehaviour
{
    public Monster monster;

    private ConclusionsManager conManager;

    void Start()
    {
        conManager = GameObject.Find(GAMECONTROLLER_NAME).GetComponent<ConclusionsManager>();
    }

    private void OnMouseDown()
    {
        conManager.SpotlightMonster(GetComponent<Monster>());
    }
}
