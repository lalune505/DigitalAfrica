using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMaskManager : MonoBehaviour, IUserInteractable
{
    public void HandleInputOccur(RaycastHit hit)
    {
        hit.collider.GetComponent<AudioSource>().Play();
    }
}
