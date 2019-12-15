using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] private AudioClip animalAudio;

    public AudioClip GetAnimalAudio()
    {
        return animalAudio;
    }
}
