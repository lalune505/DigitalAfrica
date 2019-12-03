using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionsContainer : MonoBehaviour
{
    public List<GameObject> conditions;
    
    public void SetRandomConditions()
    {
        foreach (var item in conditions)
        {
            item.SetActive(false);
        }
        conditions[Random.Range(0, conditions.Count)].SetActive(true);
    }
}
