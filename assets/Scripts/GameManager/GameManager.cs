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
    public event Action stopGame;

    [field: SerializeField] public Player Player { get; private set; }

    private void Awake()
    {
        InitializeSingleton();
    }

    private void Start()
    {

        DeviceManager.Instance.OnLocationStatusChanged += OnLocationStatusChanged;
        
        preInitialize += DeviceManager.Instance.Initialize;

        preInitialize?.Invoke();
    }

    private void OnLocationStatusChanged(LocationServiceStatus status)
    {
        if (status == LocationServiceStatus.Stopped)
        {
            Debug.Log("LocationService is disabled, unfortunately we have to stop the game.");
            stopGame?.Invoke();
        }
    }

    private void InitializeSingleton()
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
