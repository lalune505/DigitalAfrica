using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionsContainer : MonoBehaviour
{
    public List<ParticleSystem> conditions;
    
    public void SetRandomConditions()
    {
        foreach (var item in conditions)
        {
            item.Stop();
        }
        conditions[Random.Range(0, conditions.Count)].Play();
    }
}
