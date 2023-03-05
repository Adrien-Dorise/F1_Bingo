using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class race : MonoBehaviour
{

    [SerializeField] private List<Sprite> bingoImage;
    private List<GameObject> buttons;
    private List<int> selectedbuttons;
    private GameObject RDVvirage;
    private int bingoState;


    private void Start()
    {
        Debug.Log(PlayerPrefs.GetString(Save.race));
        Debug.Log(PlayerPrefs.GetString(Save.racestatus));

        bingoState = 0;
        RDVvirage = GameObject.Find("RDVvirage");
        buttons = new List<GameObject>();
        selectedbuttons = new List<int>();
        
        string savedButt = PlayerPrefs.GetString(Save.race); 
        string savedButtStates = PlayerPrefs.GetString(Save.racestatus); 
        int id = 0;
        foreach(Button butt in GameObject.Find("Buttons").transform.GetComponentsInChildren<Button>())
        {
            buttons.Add(butt.gameObject);
            butt.image.sprite = bingoImage[int.Parse(savedButt.Split(' ')[id+1])];
            if(savedButtStates[id] == 0)
            {
                butt.image.color = Color.white;
            }
            else
            {
                selectedbuttons.Add(id);
                butt.image.color = Color.green;
            }
        }
    }


/*

    public void bingoButton(int ID)
    {
        
        if(!selectedButtons.Contains(ID)) //The button was previously off and put on on click
        {
            selectedButtons.Add(ID);
            buttons[ID-1].GetComponent<Image>().color = Color.green;

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
            validationButton.GetComponent<Image>().color = Color.green;
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
            validationButton.GetComponentInChildren<Text>().text = "Selectionnes 6 bingos !";
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

*/

    public void backButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}