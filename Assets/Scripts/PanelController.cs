using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField] private bool enableOnStart;
    [SerializeField] private float fadeTime;
    [SerializeField] private float fadeSpeed;
    
    private GameObject _panel;

     private void Awake()
     {
         _panel = this.gameObject;
     }

     private void Start()
     {
         EnablePanel(enableOnStart);
     }

     private IEnumerator FadeTextColor(Text text, Color aColor, Color bColor, bool fade)
    {
        var t = 0.0f;
        var rate = (1.0f / fadeTime) * fadeSpeed;
        
        if (fade)
        {
            text.enabled = true;
        }
        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            text.color = Color.Lerp(aColor, bColor , t);
            yield return null;
        }
        if (!fade)
        {
            text.enabled = false;
        }
    }
        
    private IEnumerator FadeImageColor(Image image, Color aColor, Color bColor, bool fade)
    {
        var t = 0.0f;
        var rate = (1.0f / fadeTime) * fadeSpeed;

        if (fade)
        {
            image.enabled = true;
        }

        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            image.color = Color.Lerp(aColor, bColor , t);
            yield return null;
        }

        if (!fade)
        {
            image.enabled = false;
        }
        
    }

    public void FadePanel(bool fade)
    {
        var textComps = _panel.GetComponentsInChildren<Text>();
        var imageComps = _panel.GetComponentsInChildren<Image>();

        if (textComps.Length != 0)
        {
            foreach (var text in textComps)
            {
                var startColor = text.color;
                var endColor = startColor;
                endColor.a =  fade ? 1f : 0f;
                StartCoroutine(FadeTextColor(text, startColor, endColor, fade));
            }
        }

        if (imageComps.Length != 0)
        {
            foreach (var image in imageComps)
            {
                var startColor = image.color;
                var endColor = startColor;
                endColor.a = fade ? 1f : 0f;
                StartCoroutine(FadeImageColor(image, startColor, endColor, fade));
            }
        }
    }

    public void EnablePanel(bool enable)
    {
        var textComps = _panel.GetComponentsInChildren<Text>();
        var imageComps = _panel.GetComponentsInChildren<Image>();

        foreach (var item in textComps)
        {
            var color = item.color;
            color.a = enable ? 1f : 0f;
            item.color = color;
            item.enabled = enable;
        }
        
        foreach (var item in imageComps)
        {
            var color = item.color;
            color.a = enable ? 1f : 0f;
            item.color = color;
            item.enabled = enable;
        }
    }
}
