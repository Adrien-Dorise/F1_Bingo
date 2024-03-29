using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class <c>mainMenu</c> to attach to Game Manager gameObject of the mainMenu scene.
/// This class set all button behaviour of the main scene.
/// The buttons in this class are pretty straightforward, as they either enable new canvases, or load scenes.
/// </summary>
public class mainMenu : MonoBehaviour
{
    public GameObject menuCanvas, optionCanvas, confirmationCanvas; //Reference to all canvases use din the main screen
    private string optionToReset; //Store the selected playerPref setting to reset when arriving in the confirmation screen
    private AudioSource audioObject;

    private void Start()
    {
        menuCanvas.SetActive(true);
        optionCanvas.SetActive(false);
        confirmationCanvas.SetActive(false);
        optionToReset = "";
        audioObject = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey(Save.isMusic))
        {
            PlayerPrefs.SetInt(Save.isMusic, 1);
            audioObject.volume = 1;
        }
        else
        {
            audioObject.volume = PlayerPrefs.GetInt(Save.isMusic);
        }
    }

    public void raceButton()
    {
        if(PlayerPrefs.HasKey(Save.race))
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
        optionToReset = Save.race;
    }
    
    public void resetSeasonButton()
    {
        optionCanvas.SetActive(false);
        confirmationCanvas.SetActive(true);
        optionToReset = Save.season;
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
        menuCanvas.SetActive(true);
    }
    
    public void noButton()
    {
        confirmationCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void musicButton()
    {
        if (PlayerPrefs.GetInt(Save.isMusic) == 1)
        {
            PlayerPrefs.SetInt(Save.isMusic,0);
            audioObject.volume = 0;
        }
        else
        {
            PlayerPrefs.SetInt(Save.isMusic,1);
            audioObject.volume = 1;
        }
    }

}
