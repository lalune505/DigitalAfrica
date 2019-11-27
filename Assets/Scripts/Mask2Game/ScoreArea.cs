using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreArea : MonoBehaviour
{
    private Animator maskAnimator;
    [HideInInspector]
    public bool collided;
    private void OnTriggerEnter(Collider other)
    {
        if (!collided)
        {
            collided = true;
            maskAnimator.SetTrigger("Hit");
        }
    }

    private void Awake()
    {
        maskAnimator = GetComponentInParent<Animator>();
    }
}
