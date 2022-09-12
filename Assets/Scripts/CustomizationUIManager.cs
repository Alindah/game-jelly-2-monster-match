using UnityEngine;
using TMPro;

public class CustomizationUIManager : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField ageField;

    private void Start()
    {
        nameField.text = SaveManager.playerName;
        ageField.text = SaveManager.playerAge;
    }
}
