using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalsCanvasController : MonoBehaviour
{
    public GameObject animalNameTextGo;
    public GameObject targetPanel;
    public GameObject soundButton;
    public Sprite muteSprite;
    public Sprite unMuteSprite;
    public void EnableTargetPanel(bool show)
    {
        targetPanel.SetActive(show);
    }

    public void MuteSound()
    {
        SoundManager.instance.MuteSound();

        soundButton.GetComponent<Image>().sprite = SoundManager.instance.IsMute() ? muteSprite : unMuteSprite;
    }

    public void EnableButton(bool enable)
    {
        soundButton.SetActive(enable);
    }
}
