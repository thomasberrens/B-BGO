using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;

    //assign at the on click button event in UI 
    //make sure to add the scenes in build settings
    public void ChangeToMainGame()
    {
        gameManager.GetComponent<SceneSwitcher>().SwitchScene("MainScene");    
    }

    public void ChangeToSettings()
    {
        gameManager.GetComponent<SceneSwitcher>().SwitchScene("SettingsScene");
    }
}
