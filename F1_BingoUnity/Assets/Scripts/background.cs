using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class background : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    // We set the correct ratio for background image
    void Start()
    {
        
        Sprite backSprite = sprites[UnityEngine.Random.Range(0,sprites.Count)];
        this.GetComponent<Image>().sprite = backSprite;
        this.GetComponent<AspectRatioFitter>().aspectRatio = backSprite.bounds.size.x / backSprite.bounds.size.y;
    }

}
