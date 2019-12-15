using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsManager : MonoBehaviour, IUserInteractable
{
    private string[] triggers = {"Touch1", "Touch2"};

    public void HandleInputOccur(RaycastHit hit)
    {
        hit.collider.GetComponent<Animator>().SetTrigger(triggers[Random.Range(0, triggers.Length)]);
    }
}
