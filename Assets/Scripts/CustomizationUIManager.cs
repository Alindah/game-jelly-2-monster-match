using System.IO;
using System.Text.RegularExpressions;
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

    [Header("TRAITS")]
    public GameObject traitToggleObj;
    public Transform traitsTransform;
    public float traitsToggleSpacing;

    [Header("PARTS")]
    public GameObject partsDropdownObj;
    public Transform appearanceTransform;
    public Transform baseTransform;
    public float partsDropdownSpacing;

    [Header("COLORS")]
    public Color defaultTextColor;
    public Color darkTextColor;
    public Color highlightedTextColor;
    public Color disabledTextColor;

    [Header("Color Sliders")]
    public Slider sliderRed;
    public Slider sliderGreen;
    public Slider sliderBlue;

    [Header("UI Objects")]
    public TMP_Text portraitHeader;
    public TMP_Text portraitTraits;
    public Button finishButton;

    private TMP_Dropdown[] partsDropdowns;
    private Toggle[] traitToggles;
    private bool fullTraits = false;
    private Color disabledButtonTextColor;
    private SpriteRenderer[] baseParts;

    private string PORTRAIT_HEADER_TEXT;
    private string TRAITS_UI_TEXT;

    private void Start()
    {
        // Set text templates
        TRAITS_UI_TEXT = traitsTransform.GetComponentInChildren<TMP_Text>().text;
        PORTRAIT_HEADER_TEXT = portraitHeader.text;

        // Set initial info
        nameField.text = player.name;
        ageField.text = player.age;

        disabledButtonTextColor = finishButton.GetComponentInChildren<TMP_Text>().color;

        UpdatePortraitTraits();
        PopulateTraitsToggles();
        PopulatePartsDropdown();
        SetInitialColorSlider(RandomizeBaseColor());
        SetBaseColor();
    }

    // Make sure player is valid before allowing user to continue
    public bool PlayerIsValid()
    {
        return player.name != "" && player.age != "" && fullTraits; 
    }

    public void UpdateTraitsCountUI()
    {
        traitsTransform.GetComponentInChildren<TMP_Text>().text = string.Format(TRAITS_UI_TEXT, player.traits.Count, numOfTraits);
    }

    // Update name on portrait as player types in name
    public void UpdateInfoText()
    {
        portraitHeader.text = string.Format(PORTRAIT_HEADER_TEXT, nameField.text, ageField.text);
    }

    // Save player info
    public void SaveInfo()
    {
        player.name = nameField.text;
        player.age = ageField.text;
        SetButtonState();
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

        if (player.traits.Count <= numOfTraits)
        {
            // Determine what happens when clicked
            if (toggle.isOn)
            {
                // Add traits to respective lists
                player.traits.Add(toggleIndex);
                player.opposingTraits.Add(opposingTraitIndex);

                // Disable opposing trait's toggle
                traitToggles[opposingTraitIndex].interactable = false;

                // Set appropriate colors
                traitToggles[toggleIndex].GetComponentInChildren<Text>().color = highlightedTextColor;
                traitToggles[opposingTraitIndex].GetComponentInChildren<Text>().color = disabledTextColor;
            }
            else
            {
                // Remove traits from respective lists
                player.traits.Remove(toggleIndex);
                player.opposingTraits.Remove(opposingTraitIndex);

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
                        if (!player.traits.Contains(i) && !player.opposingTraits.Contains(i))
                        {
                            traitToggles[i].interactable = true;
                            traitToggles[i].GetComponentInChildren<Text>().color = defaultTextColor;
                        }
                    }
                }
            }
        }

        fullTraits = player.traits.Count >= numOfTraits;
        UpdateTraitsCountUI();

        // Disable all other traits if limit is reached
        if (fullTraits)
        {
            for (int i = 0; i < traitToggles.Length; i++)
            {
                if (!player.traits.Contains(i))
                {
                    traitToggles[i].interactable = false;
                    traitToggles[i].GetComponentInChildren<Text>().color = disabledTextColor;
                }
            }
        }

        UpdatePortraitTraits();
        SetButtonState();
    }

    private void UpdatePortraitTraits()
    {
        string portraitText = "";

        foreach (int i in player.traits)
            portraitText += traits[i] + "\n";

        portraitTraits.text = portraitText;
    }

    private void SetButtonState()
    {
        if (PlayerIsValid())
        {
            finishButton.GetComponentInChildren<TMP_Text>().color = defaultTextColor;
            finishButton.interactable = true;
        }
        else
        {
            finishButton.GetComponentInChildren<TMP_Text>().color = disabledButtonTextColor;
            finishButton.interactable = false;
        }
    }

    // Populate dropdown with body parts
    public void PopulatePartsDropdown()
    {
        partsDropdowns = new TMP_Dropdown[3];
        string[] partsPaths = Directory.GetDirectories(PARTS_PATH);
        string[] partsCategory = new string[partsPaths.Length];

        // Get category names
        for (int i = 0; i < partsCategory.Length; i++)
        {
            Regex regex = new Regex(PARTS_PATH + '/');
            partsCategory[i] = regex.Replace(partsPaths[i], "");
        }

        // Create new dropdown objects depending on number of traits indicated
        for (int i = 0; i < partsCategory.Length; i++)
        {
            GameObject obj = Instantiate(partsDropdownObj,
                new Vector2(appearanceTransform.position.x, appearanceTransform.position.y + partsDropdownSpacing * i),
                Quaternion.identity,
                appearanceTransform);

            obj.transform.GetComponentInChildren<TMP_Text>().text = partsCategory[i];

            partsDropdowns[i] = obj.GetComponent<TMP_Dropdown>();

            //partsDropdowns[i].AddOptions(traits);
            //partsDropdowns[i].value = player.traits[i];
        }
    }

    private Color RandomizeBaseColor()
    {
        float randomRed = Random.Range(0, 1.0f);
        float randomGreen = Random.Range(0, 1.0f);
        float randomBlue = Random.Range(0, 1.0f);

        return new Color(randomRed, randomGreen, randomBlue, 1);
    }

    private void SetInitialColorSlider(Color color)
    {
        sliderRed.value = color.r;
        sliderGreen.value = color.g;
        sliderBlue.value = color.b;
    }

    public void SetBaseColor()
    {
        baseParts = baseTransform.GetComponentsInChildren<SpriteRenderer>();
        player.baseColor = new Color(sliderRed.value, sliderGreen.value, sliderBlue.value, 1);

        foreach (SpriteRenderer sprite in baseParts)
            sprite.color = player.baseColor;
    }

    public void FinalizePlayer()
    {
        if (PlayerIsValid())
            GameController.MoveToScene(APP_SCENE);
        else
            Debug.Log("Invalid player!");
    }
}
