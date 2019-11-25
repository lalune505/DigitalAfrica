using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimation : MonoBehaviour {
    public bool loop;
    public float frameSeconds = 1;
    private Image img;
    public Sprite [] sprites;
    private int frame = 0;
    private float deltaTime = 0;
    
    void Start () {
        img = GetComponent<Image> ();
    }
    void Update () {
       
        deltaTime += Time.deltaTime;
        while (deltaTime >= frameSeconds) {
            deltaTime -= frameSeconds;
            frame++;
            if(loop)
                frame %= sprites.Length;
            //Max limit
            else if(frame >= sprites.Length)
                frame = sprites.Length - 1;
        }
        img.sprite = sprites [frame];
    }
}