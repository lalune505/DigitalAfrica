using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource _audioSource;
     void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this)
        {
            Destroy (gameObject);
        }

        DontDestroyOnLoad (gameObject);
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetAudioClip(AudioClip clip)
    {
        _audioSource.clip = clip;
    }

    public void PlayAudioClip()
    {
        _audioSource.Play();
    }

    public void StopAudioClip()
    {
        _audioSource.Stop();
    }

    public void MuteSound()
    {
        _audioSource.mute = !_audioSource.mute;
    }

    public void PauseAudioSource()
    {
        _audioSource.Pause();
    }
    public void UnPauseAudioSource()
    {
        _audioSource.UnPause();
    }

    public bool IsMute()
    {
        return _audioSource.mute;
    }
    
    
}
