using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
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
    

    // Update is called once per frame
    void Update()
    {

    }
}
