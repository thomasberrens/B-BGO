using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string mainScene = "MainScene";
    [SerializeField] private string settingsScene = "Settings";
    
    //assign at the on click button event in UI 
    //make sure to add the scenes in build settings
    public void ChangeToMainGame()
    {
        GetComponent<SceneSwitcher>().SwitchScene(mainScene);    
    }

    public void ChangeToSettings()
    {
        GetComponent<SceneSwitcher>().SwitchScene(settingsScene);
    }
}