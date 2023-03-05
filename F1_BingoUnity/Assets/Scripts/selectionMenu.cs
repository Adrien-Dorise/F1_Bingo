using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class selectionMenu : MonoBehaviour
{
    private GameObject validationButton; 
    private List<GameObject> buttons;

     [SerializeField] public List<int> selectedButtons;

    [SerializeField] private int numberOfBingo;
    
    private void Start()
    {
        numberOfBingo = 6;

        selectedButtons = new List<int>();
        buttons = new List<GameObject>();
        validationButton = GameObject.Find("Validation").gameObject;
        foreach(Button butt in GameObject.Find("Buttons").transform.GetComponentsInChildren<Button>())
        {
            buttons.Add(butt.gameObject);
        }

    }

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

    public void GO()
    {
        SceneManager.LoadScene("Race");
    }

}
