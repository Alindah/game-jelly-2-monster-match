using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Traits;
using static SaveManager;
using static Constants;

public class CustomizationUIManager : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField ageField;
    public GameObject traitDropdownObj;
    public GameObject traitToggleObj;
    public Transform traitsTransform;
    public float traitsDropdownSpacing; // Spacing between traits dropdown boxes
    public float traitsToggleSpacing;
    public Color warningColor;

    private TMP_Dropdown[] traitDropdowns;
    private Toggle[] traitToggles;

    private void Start()
    {
        nameField.text = playerName;
        ageField.text = playerAge;
        PopulateTraitsToggles();
    }

    public void PopulateTraitsToggles()
    {
        Transform column1 = traitsTransform.Find(COLUMN1_NAME);
        Transform column2 = traitsTransform.Find(COLUMN2_NAME);

        traitToggles = new Toggle[traits.Count];

        // Populate 
        for (int i = 0; i < traits.Count / 2; i++)
        {
            GameObject obj = Instantiate(traitToggleObj,
                new Vector2(column1.position.x, column1.position.y + traitsToggleSpacing * i),
                Quaternion.identity,
                column1);

            GameObject obj2 = Instantiate(traitToggleObj,
                new Vector2(column2.position.x, column2.position.y + traitsToggleSpacing * i),
                Quaternion.identity,
                column2);

            obj.GetComponentInChildren<Text>().text = traits[i];
            obj2.GetComponentInChildren<Text>().text = traits[i + traits.Count / 2];
        }
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

        Debug.Log("dropdown " + dropdownIndex + " was changed");

        for (int i = 0; i < traitDropdowns.Length; i++)
        {
            if (i == dropdownIndex)
                continue;

            if (dropdown.value == traitDropdowns[i].value || dropdown.value == GetOppositeTraitIndex(traitDropdowns[i].value))
            {
                Debug.Log("red");
                dropdown.GetComponentInChildren<TMP_Text>().color = warningColor;
                //traitDropdowns[i].GetComponentInChildren<TMP_Text>().color = warningColor;
            }
            else
            {
                Debug.Log("fine");
                //dropdown.GetComponentInChildren<TMP_Text>().color = dropdownTextColor;
                //traitDropdowns[i].GetComponentInChildren<TMP_Text>().color = dropdownTextColor;
            }
        }
    }
}
