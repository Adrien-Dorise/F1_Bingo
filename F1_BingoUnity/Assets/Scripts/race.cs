using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Class <c>selectionMenu</c> to attach to a Game Manager gameObject in the race scene.
/// This class set all button behaviour for the race screen.
/// </summary>
public class race : MonoBehaviour
{

    [SerializeField] private List<Sprite> bingoImage; //To set in editor: List of all bingo images available
    private List<GameObject> buttons; //Reference to all butotns in the scene
    private List<int> selectedButtons; //Store the state of eac buttons in the scene
    private GameObject RDVvirage; //Reference to RDVvirage bingo element


    private void Start()
    {
        Debug.Log(PlayerPrefs.GetString(Save.race));
        Debug.Log(PlayerPrefs.GetString(Save.racestatus));

        RDVvirage = GameObject.Find("RDVvirage");
        buttons = new List<GameObject>();
        selectedButtons = new List<int>();
        
        string savedButt = PlayerPrefs.GetString(Save.race); 
        string savedButtStates = PlayerPrefs.GetString(Save.racestatus); 
        int id = 1;
        foreach(Button butt in GameObject.Find("Buttons").transform.GetComponentsInChildren<Button>())
        {
            buttons.Add(butt.gameObject);
            butt.image.sprite = bingoImage[int.Parse(savedButt.Split(' ')[id])-1];
            
            if(savedButtStates[id-1] == '0')
            {
                butt.image.color = Color.white;
            }
            else
            {
                selectedButtons.Add(id);
                butt.image.color = Color.green;
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
        System.Text.StringBuilder saveState = new System.Text.StringBuilder(PlayerPrefs.GetString(Save.racestatus));
        if(!selectedButtons.Contains(ID)) //The button was previously off and put on on click
        {
            selectedButtons.Add(ID);
            buttons[ID-1].GetComponent<Image>().color = Color.green;
            saveState[ID-1] = '1';
            PlayerPrefs.SetString(Save.racestatus, saveState.ToString());
        }
        else
        {
            buttons[ID-1].GetComponent<Image>().color = Color.white;
            saveState[ID-1] = '0';
            PlayerPrefs.SetString(Save.racestatus, saveState.ToString());
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
    /// More specifically, it verifies that the red/green lights are in check with the number of selected bingo
    /// </summary>   
    private void RDVvirageManagement()
    {
        for(int i = 0 ; i < RDVvirage.transform.childCount; i++)
        {
            if(i < selectedButtons.Count * 6)
            {
                RDVvirage.transform.GetChild(i).GetComponent<Light2D>().color = Color.green;
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