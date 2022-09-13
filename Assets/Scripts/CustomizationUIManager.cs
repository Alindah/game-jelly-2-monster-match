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

    private TMP_Dropdown[] traitDropdowns;

    private void Start()
    {
        nameField.text = playerName;
        ageField.text = playerAge;
        InitializeTraits();
        PopulateTraitsDropdown();
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
            traitDropdowns[i].value = traitsIndex[i];
        }
    }
}
