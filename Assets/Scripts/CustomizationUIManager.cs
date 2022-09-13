using UnityEngine;
using TMPro;
using static Traits;
using static SaveManager;

public class CustomizationUIManager : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField ageField;
    public GameObject traitDropdownObj;
    public Transform traitsTransform;
    public float traitsDropdownSpacing; // Spacing between traits dropdown boxes
    public Color warningColor;

    private TMP_Dropdown[] traitDropdowns;
    private bool validPlayer = true;
    private Color dropdownColor;
    private Color dropdownTextColor;

    private void Start()
    {
        nameField.text = playerName;
        ageField.text = playerAge;
        InitializeTraits();
        PopulateTraitsDropdown();
        dropdownColor = traitDropdowns[0].colors.normalColor;
        dropdownTextColor = traitDropdowns[0].GetComponentInChildren<TMP_Text>().color;
    }

    // Populate dropdown with traits
    public void PopulateTraitsDropdown()
    {
        traitDropdowns = new TMP_Dropdown[3];

        // Create new dropdown objects depending on number of traits indicated
        for (int i = 0; i < GameConfig.numOfTraits; i++)
        {
            GameObject obj = Instantiate(traitDropdownObj,
                new Vector2(traitsTransform.position.x, traitsTransform.position.y + traitsDropdownSpacing * i),
                Quaternion.identity,
                traitsTransform);

            traitDropdowns[i] = obj.GetComponent<TMP_Dropdown>();
            traitDropdowns[i].AddOptions(traits);
            traitDropdowns[i].value = playerTraits[i];
        }

        foreach (TMP_Dropdown dd in traitDropdowns)
        {
            dd.onValueChanged.AddListener(delegate
            {
                UpdateTraitsDropdown(dd);
            });
        }
    }

    // Check if dropdown contains duplicate traits or opposing traits and show in red if so
    public void UpdateTraitsDropdown(TMP_Dropdown dropdown)
    {
        int dropdownIndex = System.Array.IndexOf(traitDropdowns, dropdown);
        playerTraits[dropdownIndex] = dropdown.value;

        for (int i = 0; i < traitDropdowns.Length; i++)
        {
            if (i == dropdownIndex)
                continue;

            if (dropdown.value == traitDropdowns[i].value || dropdown.value == GetOppositeTraitIndex(traitDropdowns[i].value))
            {
                Debug.Log("red");
                dropdown.GetComponentInChildren<TMP_Text>().color = warningColor;
                traitDropdowns[i].GetComponentInChildren<TMP_Text>().color = warningColor;
            }
            else
            {
                Debug.Log("fine");
                dropdown.GetComponentInChildren<TMP_Text>().color = dropdownTextColor;
                traitDropdowns[i].GetComponentInChildren<TMP_Text>().color = dropdownTextColor;
            }
        }
    }
}
