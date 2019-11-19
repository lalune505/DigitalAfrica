using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetContentManager : MonoBehaviour
{
    private static Target _currentTarget;
    private static bool _isHiding;
    private static GameObject _transitionGO;
    private static bool isNextPrefab;
    private static Text nameTextField;
    void Awake()
    {
        nameTextField = FindObjectOfType<AnimalsCanvasController>().animalNameTextGo.GetComponent<Text>();
        UpdateNameTextField("");
    }

    public static void SetCurrentTarget(Target target, GameObject go)
    {
        _currentTarget = target;
        
        _currentTarget.HideAllPrefabs();

        SetTransitionGO(go);
    }

    public static void ActivateTargetPrefab()
    {
        _currentTarget.GetCurrentTargetPrefab().SetActive(true);

        UpdateNameTextField(_currentTarget.GetCurrentTargetPrefab().GetComponent<AnimalName>().GetAnimalName());

        _isHiding = false;
    }
    private static void SetTransitionGO(GameObject go)
    {
        _transitionGO = go;
    }
    public static void ShowNext()
    {
        if (_isHiding == false)
        {
            _isHiding = true;
            
            _transitionGO.SetActive(true);

            isNextPrefab = true;
        }

    }
    public static void ShowPrev()
    {
        if (_isHiding == false)
        {
            _isHiding = true;
            
            _transitionGO.SetActive(true);

            isNextPrefab = false;
        }

    }
    public static void HidePrefabAnimationEvent()
    {
        _isHiding = false;
        
        _transitionGO.SetActive(false);

    }

    public static void SwapPrefabsAnimationEvent()
    {
        _currentTarget.GetCurrentTargetPrefab().SetActive(false);
        
        if (isNextPrefab)
        {
            _currentTarget.SetNextTargetPrefabIndex();
        }
        else
        {
            _currentTarget.SetPrevTargetPrefabIndex();
        }
        
        _currentTarget.GetCurrentTargetPrefab().SetActive(true);
        
        UpdateNameTextField(_currentTarget.GetCurrentTargetPrefab().GetComponent<AnimalName>().GetAnimalName());
    }

    public static void UpdateNameTextField(string text)
    {
        nameTextField.text = text;
    }
}

public class Target
{
    private readonly List<GameObject> _cubePrefabs;

    private int _currentPrefabIndex;

    public Target(List<GameObject> prefabs, int currentPrefabIndex)
    {
        _cubePrefabs = prefabs;

        _currentPrefabIndex = currentPrefabIndex;
    }
    public GameObject GetCurrentTargetPrefab()
    {
        return _cubePrefabs[_currentPrefabIndex];
    }

    public void SetNextTargetPrefabIndex()
    { 
        _currentPrefabIndex = (_currentPrefabIndex + 1) % _cubePrefabs.Count;
    }

    public void SetPrevTargetPrefabIndex()
    {
        _currentPrefabIndex = (_cubePrefabs.Count + _currentPrefabIndex - 1) % _cubePrefabs.Count;
    }

    public void HideAllPrefabs()
    {
        foreach (var item in _cubePrefabs)
        {
            item.SetActive(false);
        }
    }
}
