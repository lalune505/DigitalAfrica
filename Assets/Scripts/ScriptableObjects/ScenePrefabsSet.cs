using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
[CreateAssetMenu(fileName = "ScenePrefabsSet", menuName = "ScriptableObjects/ScenePrefabs", order = 100)]
public class ScenePrefabsSet : ScriptableObject
{
    [SerializeField]
    public List<GameObject> targets;
}
