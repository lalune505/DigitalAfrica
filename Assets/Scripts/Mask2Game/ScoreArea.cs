using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreArea : MonoBehaviour
{
    [HideInInspector]
    public bool collided;
    private void OnTriggerEnter(Collider other)
    {
        if (!collided)
        {
            collided = true;
            Debug.Log("Collided");
        }
    }
    
}
