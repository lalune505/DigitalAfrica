using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalsCanvasController : MonoBehaviour
{
    public GameObject animalNameTextGo;
    public GameObject switcherButtons;
    public GameObject targetPanel;
    public GameObject soundButton;
    public Sprite muteSprite;
    public Sprite unMuteSprite;
    
    public void ShowNextPrefab()
    {
        TargetContentManager.ShowNext();
    }
    
    public void ShowPrevPrefab()
    {
        TargetContentManager.ShowPrev();
    }    
    public void EnablePrefabSwitcherButtons(bool activate)
    {
        switcherButtons.SetActive(activate);
    }
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
