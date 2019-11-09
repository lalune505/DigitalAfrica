using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPrefabsContainer : MonoBehaviour
{
    
    [SerializeField] private GameObject transitionPrefab;

    private Target _target;
    
    public void SetTarget(List<GameObject> targetPrefabs, int currentPrefabIndex)
    {
        _target = new Target(targetPrefabs, currentPrefabIndex);
    }

    public Target GetTarget()
    {
        return _target;
    }

    public GameObject GetTransitionPrefab()
    {
        return transitionPrefab;
    }
}
