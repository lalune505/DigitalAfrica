using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask1AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }
    
    public void StopSound()
    {
        _audioSource.Stop();
    }
}
