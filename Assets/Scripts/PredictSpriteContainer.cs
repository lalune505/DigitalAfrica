using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictSpriteContainer : MonoBehaviour
{
    [SerializeField] private Sprite[] predictSprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public void SetNewSprite()
    {
        _spriteRenderer.sprite = predictSprites[Random.Range(0, predictSprites.Length)];
    }
}
