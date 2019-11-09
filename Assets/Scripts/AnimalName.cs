using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalName : MonoBehaviour
{
   [SerializeField] private string _name;

    public string GetAnimalName()
    {
        return _name;
    }
}
