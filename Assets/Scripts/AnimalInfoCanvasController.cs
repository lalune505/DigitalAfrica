using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInfoCanvasController : MonoBehaviour
{
    public AudioClip animalClip;
    public void PlayAnimalAudio()
    {
        SoundManager.instance.SetAudioClip(animalClip);
        SoundManager.instance.PlayAudioClip();
    }
}
