using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Class <c>selectionMenu</c> to attach to a Game Manager gameObject in the selectionMenu scene.
/// This class set all button behaviour for the selection screen.
/// </summary>
public class selectionMenu : MonoBehaviour
{
    private GameObject validationButton; //Reference to the validation button object 
    private List<GameObject> buttons; //Reference to all buttons displayed in the scene

     [SerializeField] public List<int> selectedButtons; //Reference to all selected buttons in the screen

    [SerializeField] private int numberOfBingo; //To set in Start(), bingo threshold to start the race Bingo
    

    /// <summary>
    /// Method <c>Start</c> Initalise the buttons available in the scene as well as state variables.
    /// </summary>
    private void Start()
    {
        numberOfBingo = 4;

        selectedButtons = new List<int>();
        buttons = new List<GameObject>();
        validationButton = GameObject.Find("Validation").gameObject;
        foreach(Button butt in GameObject.Find("Buttons").transform.GetComponentsInChildren<Button>())
        {
            buttons.Add(butt.gameObject);
        }
        validationButton.GetComponentInChildren<Text>().text = "Selectionnes " + numberOfBingo + " bingos !";
    }

    /// <summary>
    /// Method <c>bingoButton</c> set the bingo buttons behaviour.
    /// When a button is clicked, we check if this button was already selected before by comparing its ID with saved IDs.
    /// If not clicked before, the button is added to the ID saver variable, otherwise, it is removed from it.
    /// We also verify that the max selection treshold is not reached.
    /// </summary>
    public void bingoButton(int ID)
    {
        
        if(!selectedButtons.Contains(ID)) //The button was previously off and put on on click
        {
            selectedButtons.Add(ID);
            buttons[ID-1].GetComponent<Image>().color = new Color(0.4f,1f,0.4f,1f);

        }
        else
        {
            buttons[ID-1].GetComponent<Image>().color = Color.white;
            try
            {   
                selectedButtons.Remove(ID);
            }
            catch(System.Exception e)
            {
                Debug.Log(e);
            }
        }


        // In case we selected all buttons.
        if(selectedButtons.Count >= numberOfBingo)
        {
            validationButton.GetComponentInChildren<Text>().text = "RDV au premier virage ?";
            validationButton.GetComponent<Image>().color = new Color(0.4f,1f,0.4f,1f);
            validationButton.GetComponent<Image>().raycastTarget = true;

            for(int i = 0; i < buttons.Count; i ++)
            {
                if(!selectedButtons.Contains(i+1)) //We only mask not selected buttons
                {
                    buttons[i].GetComponent<Button>().interactable = false;
                    Color col = buttons[i].GetComponent<Image>().color;
                    col.a = 0.5f;
                    buttons[i].GetComponent<Image>().color = col;
                }
            }
        }
        else
        {
            validationButton.GetComponentInChildren<Text>().text = "Selectionnes " + numberOfBingo + " bingos !";
            validationButton.GetComponent<Image>().color = Color.white;
            validationButton.GetComponent<Image>().raycastTarget = false;

            foreach(GameObject butt in buttons)
            {
                butt.GetComponent<Button>().interactable = true;
                Color col = butt.GetComponent<Image>().color;
                col.a = 1f;
                butt.GetComponent<Image>().color = col;
            }
        }
    }

    /// <summary>
    /// Method <c>GO</c> set the switch to the race scene when bingos are selected
    /// </summary>
    public void GO()
    {
        if(selectedButtons.Count == numberOfBingo)
        {
            string saveRace = "";
            string saveStatus = "";
            foreach(int id in selectedButtons)
            {
                saveRace = saveRace + " " + id;
                saveStatus += "0";
            }
            PlayerPrefs.SetString(Save.race, saveRace);
            PlayerPrefs.SetString(Save.racestatus, saveStatus);
            SceneManager.LoadScene("Race");
        }
    }

}
