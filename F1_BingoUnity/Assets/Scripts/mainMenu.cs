using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject menuCanvas, optionCanvas, confirmationCanvas;
    private string optionToReset;

    private void Start()
    {
        menuCanvas.SetActive(true);
        optionCanvas.SetActive(false);
        confirmationCanvas.SetActive(false);
        optionToReset = "";
    }

    public void raceButton()
    {
        if(PlayerPrefs.HasKey("race"))
        {
            SceneManager.LoadScene("Race");
        }
        else
        {
            SceneManager.LoadScene("Selection");
        }
    }
    
    
    public void seasonButton()
    {
        SceneManager.LoadScene("Season");
        
    }
    
    public void optionButton()
    {
        menuCanvas.SetActive(false);
        optionCanvas.SetActive(true);
    }
    
    public void resetRaceButton()
    {
        optionCanvas.SetActive(false);
        confirmationCanvas.SetActive(true);
        optionToReset = "race";
    }
    
    public void resetSeasonButton()
    {
        optionCanvas.SetActive(false);
        confirmationCanvas.SetActive(true);
        optionToReset = "season";
    }
    
    public void backButton()
    {
        optionCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }
    
    public void yesButton()
    {
        PlayerPrefs.DeleteKey(optionToReset);
        confirmationCanvas.SetActive(false);
        optionCanvas.SetActive(true);
    }
    
    public void noButton()
    {
        confirmationCanvas.SetActive(false);
        optionCanvas.SetActive(true);
    }

}
