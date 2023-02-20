using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private List<GameObject> staticObjects = new List<GameObject>();
    
    public void SwitchScene(string sceneName) {
        staticObjects.ForEach(staticObject => DontDestroyOnLoad(staticObject)); ;
        SceneManager.LoadScene(sceneName);
    }

    public void Awake()
    {
        SwitchScene("TestScene");
    }
}
