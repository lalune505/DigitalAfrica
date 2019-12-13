using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictionMaskManager : MonoBehaviour, IUserInteractable
{
    public void HandleInputOccur(RaycastHit hit)
    {
        hit.collider.GetComponent<Animator>().SetTrigger("Touch");
        hit.collider.GetComponent<PredictSpriteContainer>().SetNewSprite();
    }
}
