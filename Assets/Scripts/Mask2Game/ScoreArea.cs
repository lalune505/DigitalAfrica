using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreArea : MonoBehaviour
{
    public ParticleSystem particles;
    private Animator maskAnimator;
    [HideInInspector]
    public bool collided;
    private void OnTriggerEnter(Collider other)
    {
        if (!collided)
        {
            collided = true;
            particles.Play();
            maskAnimator.SetTrigger("Hit");
        }
    }

    private void Awake()
    {
        maskAnimator = GetComponentInParent<Animator>();
    }
}
