using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskManager : MonoBehaviour, IUserInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleInputOccur(RaycastHit hit)
    {
        hit.collider.GetComponent<Animator>().SetTrigger("Touch");
    }
}
