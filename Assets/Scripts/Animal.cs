using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
   [SerializeField] private string animalName;
   [SerializeField] private AudioClip animalAudio;

    public string GetAnimalName()
    {
        return animalName;
    }

    public AudioClip GetAnimalAudio()
    {
        return animalAudio;
    }
}
