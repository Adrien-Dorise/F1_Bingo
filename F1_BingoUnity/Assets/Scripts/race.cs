using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Class <c>selectionMenu</c> to attach to a Game Manager gameObject in the race/season scene.
/// This class set all button behaviour for the race screen.
/// </summary>
public class race : MonoBehaviour
{

    [SerializeField] private List<Sprite> bingoImage; //To set in editor: List of all bingo images available
    public List<GameObject> buttons; //Reference to all butotns in the scene
    private List<int> selectedButtons; //Store the state of eac buttons in the scene
    private GameObject RDVvirage; //Reference to RDVvirage bingo element
    private bool isRace; //true if we are on race screen, false for season screen
    private string saveStatus; //Store the playerkeys value according to current screen (race / season)
    private int lightsPerBingo; //Numbe of lights lit per bingo selected


    private void Start()
    {
        isRace = SceneManager.GetActiveScene().name == "Race";
        if(isRace)
        {
            lightsPerBingo = 6;
            saveStatus = Save.racestatus;
            Debug.Log(PlayerPrefs.GetString(Save.race));
        }
        else
        {
            lightsPerBingo = 1;
            saveStatus = Save.season;
            if(!PlayerPrefs.HasKey(Save.season)) //In case there is not season save stored
            {
                PlayerPrefs.SetString(Save.season, "".PadRight(GameObject.Find("Buttons").transform.childCount, '0')); //Add as many zeros as buttons on the scene.
            }        
        }
        Debug.Log(PlayerPrefs.GetString(saveStatus));

        RDVvirage = GameObject.Find("RDVvirage");
        buttons = new List<GameObject>();
        selectedButtons = new List<int>();
        

        string savedButtStates = PlayerPrefs.GetString(saveStatus); 
        int id = 1;
        foreach(Button butt in GameObject.Find("Buttons").transform.GetComponentsInChildren<Button>())
        {
            buttons.Add(butt.gameObject);
            if(isRace)
            {
                butt.image.sprite = bingoImage[int.Parse(PlayerPrefs.GetString(Save.race).Split(' ')[id])-1];
            }
            
            if(savedButtStates[id-1] == '0')
            {
                butt.image.color = Color.white;
            }
            else
            {
                selectedButtons.Add(id);
                butt.image.color = new Color(0.4f,1f,0.4f,1f);
            }
            id++;
        }
        
        RDVvirageManagement();
    }


    
    /// <summary>
    /// Method <c>bingoButton</c> set the bingo buttons behaviour.
    /// When a button is clicked, we check if this button was already selected before by comparing its ID with saved IDs.
    /// If not clicked before, the button is added to the ID saver variable, otherwise, it is removed from it.
    /// PlayerPrefs state is also managed to save the modification.
    /// </summary>
    public void bingoButton(int ID)
    {
        System.Text.StringBuilder saveState = new System.Text.StringBuilder(PlayerPrefs.GetString(saveStatus));
        if(!selectedButtons.Contains(ID)) //The button was previously off and put on on click
        {
            selectedButtons.Add(ID);
            buttons[ID-1].GetComponent<Image>().color = new Color(0.4f,1f,0.4f,1f);
            saveState[ID-1] = '1';
            PlayerPrefs.SetString(saveStatus, saveState.ToString());
        }
        else
        {
            buttons[ID-1].GetComponent<Image>().color = Color.white;
            saveState[ID-1] = '0';
            PlayerPrefs.SetString(saveStatus, saveState.ToString());
            try
            {   
                selectedButtons.Remove(ID);
            }
            catch(System.Exception e)
            {
                Debug.Log(e);
            }
        }
        RDVvirageManagement();
    }

    
    /// <summary>
    /// Method <c>RDVvirageManagement</c> manage the RDVvirage bingo card. This function is to call after each bingo card click.
    /// More specifically, it verifies that the red/green lights are in check with the number of selected bingos
    /// </summary>   
    private void RDVvirageManagement()
    {
        for(int i = 0 ; i < RDVvirage.transform.childCount; i++)
        {
            if(i < selectedButtons.Count * lightsPerBingo)
            {
                RDVvirage.transform.GetChild(i).GetComponent<Light2D>().color = new Color(0.4f,1f,0.4f,1f);
            }
            else
            {
                RDVvirage.transform.GetChild(i).GetComponent<Light2D>().color = Color.red;
            }
        }
    }

    public void backButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}