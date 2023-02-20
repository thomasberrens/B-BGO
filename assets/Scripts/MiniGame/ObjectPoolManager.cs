using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _poolCache;

    private List<GameObject> objects = new List<GameObject>();

    private void Start()
    {
        // Instantiate the initial objects
        foreach (GameObject obj in _poolCache)
        {
            obj.SetActive(false);
            objects.Add(obj);
        }
    }
}