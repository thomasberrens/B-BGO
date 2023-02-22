using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action preInitialize;
    public event Action initialized;
    public event Action startGame;

    private void Awake()
    {
        initializeSingleton();
        
        preInitialize?.Invoke();
    }


    void Start()
    {

    }

    private void initializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Game manager already initialized, destroying new instance.");
            Destroy(gameObject);
        }
    }
}
