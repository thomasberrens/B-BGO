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

        DeviceManager.Instance.OnLocationStatusChanged += OnLocationStatusChanged;
        
        preInitialize += DeviceManager.Instance.Initialize;

        preInitialize?.Invoke();
        
        Debug.Log("STARTING HIHIHIHIHIH");
        Input.location.Start();
    }

    private void OnLocationStatusChanged(LocationServiceStatus status)
    {
        Debug.Log("Location Service Status: " + status);

        if (status == LocationServiceStatus.Running)
        {
            Debug.Log("UP AND RUNNING");
        }

        if (status == LocationServiceStatus.Stopped)
        {
            Debug.Log("LocationService is disabled, unfortunately we have to stop the game.");
            stopGame?.Invoke();
        }

    }

    void Start()
    {

    }

    private void Update()
    {
        Debug.Log("Location status: " + Input.location.status);
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
