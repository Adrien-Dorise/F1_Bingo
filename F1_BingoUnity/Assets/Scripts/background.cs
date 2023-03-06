using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Class <c>background</c> to attach to a background gameObject.
/// This class setup the correct ratio of the background image.
/// </summary>
public class background : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites; // To set in editor: Lis tof sprite that will be randomly selected during play
    void Start()
    {
        
        Sprite backSprite = sprites[UnityEngine.Random.Range(0,sprites.Count)];
        this.GetComponent<Image>().sprite = backSprite;
        this.GetComponent<AspectRatioFitter>().aspectRatio = backSprite.bounds.size.x / backSprite.bounds.size.y;
    }

}
