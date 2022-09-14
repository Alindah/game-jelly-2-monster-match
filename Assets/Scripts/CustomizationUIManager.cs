using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Traits;
using static GameConfig;
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

    // Populate Traits field with toggles
    public void PopulateTraitsToggles()
    {
        Transform column1 = traitsTransform.Find(COLUMN1_NAME);
        Transform column2 = traitsTransform.Find(COLUMN2_NAME);

        traitToggles = new Toggle[traits.Count];

        // Create toggles for each 
        for (int i = 0; i < traits.Count / 2; i++)
        {
            // Instantiate toggles on first column
            GameObject obj = Instantiate(traitToggleObj,
                new Vector2(column1.position.x, column1.position.y + traitsToggleSpacing * i),
                Quaternion.identity,
                column1);

            // Instatiate toggles on second column
            GameObject obj2 = Instantiate(traitToggleObj,
                new Vector2(column2.position.x, column2.position.y + traitsToggleSpacing * i),
                Quaternion.identity,
                column2);

            // Set trait names for toggles
            obj.GetComponentInChildren<Text>().text = traits[i].Item2;
            obj2.GetComponentInChildren<Text>().text = traits[i + traits.Count / 2].Item2;

            // Store Toggles in array
            traitToggles[i] = obj.GetComponent<Toggle>();
            traitToggles[i + traits.Count / 2] = obj2.GetComponent<Toggle>();
        }

        // Create listener to listen to changes
        foreach (Toggle tog in traitToggles)
        {
            tog.onValueChanged.AddListener(delegate
            {
                OnClickTraitToggle(tog);
            });
        }
    }

    /*

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
    }*/

    // Set or unset clicked trait to 
    public void OnClickTraitToggle(Toggle toggle)
    {
        int toggleIndex = System.Array.IndexOf(traitToggles, toggle);

        if (playerTraits.Count <= numOfTraits)
        {
            if (toggle.isOn)
            {
                playerTraits.Add(toggleIndex);
                traitToggles[GetOppositeTraitIndex(toggleIndex)].interactable = false;
            }
            else
            {
                playerTraits.Remove(toggleIndex);
                traitToggles[GetOppositeTraitIndex(toggleIndex)].interactable = true;
            }
        }

        // Disable all other traits if limit is reached
        if (playerTraits.Count >= numOfTraits)
        {
            for (int i = 0; i < traitToggles.Length; i++)
            {
                if (!playerTraits.Contains(i))
                    traitToggles[i].interactable = false;
            }
        }
    }
}
