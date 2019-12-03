using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMaskManager : MonoBehaviour, IUserInteractable
{
    private readonly List<string> _triggers = new List<string>{"Touch1", "Touch2", "Touch3","Touch4", "Touch5", "Touch6", "Touch7"};
    public void HandleInputOccur(RaycastHit hit)
    {
        hit.collider.GetComponent<Animator>().SetTrigger(_triggers[Random.Range(0, _triggers.Count)]);
    }
}
