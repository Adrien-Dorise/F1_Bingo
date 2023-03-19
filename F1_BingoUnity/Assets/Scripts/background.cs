using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;


/// <summary>
/// Class <c>background</c> to attach to a background gameObject.
/// This class setup the correct ratio of the background image.
/// It also state button function of background
/// </summary>
public class background : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites; // To set in editor: Lis tof sprite that will be randomly selected during play
    private bool isBackSelected; //State if the background button is activated
    private race managerScript; //Reference to gameManager script

    void Start()
    {
        try
        {
            managerScript = GameObject.Find("Game Manager").GetComponent<race>();
        }
        catch(System.Exception e)
        {
            Debug.Log(e);
        }
        isBackSelected = false;        
        Sprite backSprite = sprites[UnityEngine.Random.Range(0,sprites.Count)];
        this.GetComponent<Image>().sprite = backSprite;
        this.GetComponent<AspectRatioFitter>().aspectRatio = backSprite.bounds.size.x / backSprite.bounds.size.y;
    }

    /// <summary>
    /// Method <c>backButton</c> is used when background is selected.
    /// The bingos are set to transparent to be able to see the back picture (only work in race or season screen)
    /// </summary>
    public void backButton()
    {
        Debug.Log("backButton");
        try
        {
            Color col;
            float alpha = 0.125f;
            if(!isBackSelected) //Not yet activated
            {
                isBackSelected = true;
                foreach(GameObject butt in managerScript.buttons)
                {
                    butt.GetComponent<Button>().interactable = false;
                    col = butt.GetComponent<Image>().color;
                    col.a = alpha;
                    butt.GetComponent<Image>().color = col;
                }

                GameObject backButt = GameObject.Find("Back");
                backButt.GetComponent<Button>().interactable = false;
                col = backButt.GetComponent<Image>().color;
                col.a = alpha;
                backButt.GetComponent<Image>().color = col;

                GameObject RDVvirage = GameObject.Find("RDVvirage");
                foreach(Light2D light in RDVvirage.GetComponentsInChildren<Light2D>())
                {
                    col = light.color;
                    col.a = alpha;
                    light.color = col;
                }
                RDVvirage.GetComponent<Button>().interactable = false;
                col = RDVvirage.GetComponent<Image>().color;
                col.a = alpha;
                RDVvirage.GetComponent<Image>().color = col;


            }
            else
            {
                isBackSelected = false;
                foreach(GameObject butt in managerScript.buttons)
                {
                    butt.GetComponent<Button>().interactable = true;
                    col = butt.GetComponent<Image>().color;
                    col.a = 1f;
                    butt.GetComponent<Image>().color = col;
                }
                
                GameObject backButt = GameObject.Find("Back");
                backButt.GetComponent<Button>().interactable = true;
                col = backButt.GetComponent<Image>().color;
                col.a = 1f;
                backButt.GetComponent<Image>().color = col;

                GameObject RDVvirage = GameObject.Find("RDVvirage");
                foreach(Light2D light in RDVvirage.GetComponentsInChildren<Light2D>())
                {
                    col = light.color;
                    col.a = 1f;
                    light.color = col;
                }
                RDVvirage.GetComponent<Button>().interactable = true;
                col = RDVvirage.GetComponent<Image>().color;
                col.a = 1f;
                RDVvirage.GetComponent<Image>().color = col;

            }
        }
        catch(System.Exception e)
        {
            Debug.Log(e);
        }
    }
    


}
