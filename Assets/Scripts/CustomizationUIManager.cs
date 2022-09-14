using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Traits;
using static GameConfig;
using static SaveManager;
using static Constants;

public class CustomizationUIManager : MonoBehaviour
{
    [Header("INFO")]
    public TMP_InputField nameField;
    public TMP_InputField ageField;
    public TMP_Text portraitHeader;
    public TMP_Text portraitTraits;

    [Header("TRAITS")]
    public GameObject traitDropdownObj;
    public GameObject traitToggleObj;
    public Transform traitsTransform;
    public float traitsDropdownSpacing; // Spacing between traits dropdown boxes
    public float traitsToggleSpacing;

    [Header("COLORS")]
    public Color defaultTextColor;
    public Color highlightedTextColor;
    public Color disabledTextColor;

    private TMP_Dropdown[] partsDropdowns;
    private Toggle[] traitToggles;
    private bool fullTraits = false;

    private string PORTRAIT_HEADER_TEXT;
    private string TRAITS_UI_TEXT;

    private void Start()
    {
        // Set text templates
        TRAITS_UI_TEXT = traitsTransform.GetComponentInChildren<TMP_Text>().text;
        PORTRAIT_HEADER_TEXT = portraitHeader.text;

        // Set initial info
        nameField.text = playerName;
        ageField.text = playerAge;

        UpdatePortraitTraits();
        PopulateTraitsToggles();
    }

    // Make sure player is valid before allowing user to continue
    public bool PlayerIsValid()
    {
        return playerName != "" && playerAge != "" && fullTraits; 
    }

    public void UpdateTraitsCountUI()
    {
        traitsTransform.GetComponentInChildren<TMP_Text>().text = string.Format(TRAITS_UI_TEXT, playerTraits.Count, numOfTraits);
    }

    // Update name on portrait as player types in name
    public void UpdateInfoText()
    {
        portraitHeader.text = string.Format(PORTRAIT_HEADER_TEXT, nameField.text, ageField.text);
    }

    // Save player info
    public void SaveInfo()
    {
        playerName = nameField.text;
        playerAge = ageField.text;
    }

    // Populate Traits field with toggles
    public void PopulateTraitsToggles()
    {
        UpdateTraitsCountUI();

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
            obj.GetComponentInChildren<Text>().text = traits[i];
            obj2.GetComponentInChildren<Text>().text = traits[i + traits.Count / 2];

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

    // Manage toggles upon clicking
    public void OnClickTraitToggle(Toggle toggle)
    {
        int toggleIndex = System.Array.IndexOf(traitToggles, toggle);
        int opposingTraitIndex = GetOppositeTraitIndex(toggleIndex);

        if (playerTraits.Count <= numOfTraits)
        {
            // Determine what happens when clicked
            if (toggle.isOn)
            {
                // Add traits to respective lists
                playerTraits.Add(toggleIndex);
                opposingTraits.Add(opposingTraitIndex);

                // Disable opposing trait's toggle
                traitToggles[opposingTraitIndex].interactable = false;

                // Set appropriate colors
                traitToggles[toggleIndex].GetComponentInChildren<Text>().color = highlightedTextColor;
                traitToggles[opposingTraitIndex].GetComponentInChildren<Text>().color = disabledTextColor;
            }
            else
            {
                // Remove traits from respective lists
                playerTraits.Remove(toggleIndex);
                opposingTraits.Remove(opposingTraitIndex);

                // Reenable opposing trait's toggle
                traitToggles[opposingTraitIndex].interactable = true;

                // Set appropriate colors
                traitToggles[toggleIndex].GetComponentInChildren<Text>().color = defaultTextColor;
                traitToggles[opposingTraitIndex].GetComponentInChildren<Text>().color = defaultTextColor;

                // Reenable disabled toggles from when traits was last full
                if (fullTraits)
                {
                    for (int i = 0; i < traitToggles.Length; i++)
                    {
                        if (!playerTraits.Contains(i) && !opposingTraits.Contains(i))
                        {
                            traitToggles[i].interactable = true;
                            traitToggles[i].GetComponentInChildren<Text>().color = defaultTextColor;
                        }
                    }
                }
            }
        }

        fullTraits = playerTraits.Count >= numOfTraits;
        UpdateTraitsCountUI();

        // Disable all other traits if limit is reached
        if (fullTraits)
        {
            for (int i = 0; i < traitToggles.Length; i++)
            {
                if (!playerTraits.Contains(i))
                {
                    traitToggles[i].interactable = false;
                    traitToggles[i].GetComponentInChildren<Text>().color = disabledTextColor;
                }
            }
        }

        UpdatePortraitTraits();
    }

    private void UpdatePortraitTraits()
    {
        string portraitText = "";

        foreach (int i in playerTraits)
            portraitText += traits[i] + "\n";

        portraitTraits.text = portraitText;
    }

    /*
    // Populate dropdown with body parts
    public void PopulatePartsDropdown()
    {
        traitDropdowns = new partsDropdowns[3];

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

    public void FinalizePlayer()
    {
        if (PlayerIsValid())
            GameController.MoveToScene(APP_SCENE);
        else
            Debug.Log("Invalid player!");
    }
}
