using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MinigameManager))]
public class SerializableDictionaryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MinigameManager miniGameManager = (MinigameManager)target;

        Dictionary<GameObject, Minigame> minigamesCopy = new Dictionary<GameObject, Minigame>(miniGameManager.GetMiniGames());

        EditorGUI.BeginChangeCheck();

        foreach (var pair in minigamesCopy)
        {
            EditorGUILayout.BeginHorizontal();

            GameObject newKey = (GameObject)EditorGUILayout.ObjectField(pair.Key, typeof(GameObject), true);
            Minigame newValue = (Minigame)EditorGUILayout.ObjectField(pair.Value, typeof(Minigame), false);

            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                miniGameManager.GetMiniGames().Remove(pair.Key);
                break;
            }

            EditorGUILayout.EndHorizontal();

            if (newKey != pair.Key)
            {
                if (miniGameManager.GetMiniGames().ContainsKey(newKey))
                {
                    Debug.LogWarningFormat("Dictionary already contains key {0}", newKey);
                }
                else
                {
                    miniGameManager.GetMiniGames().Remove(pair.Key);
                    miniGameManager.GetMiniGames().Add(newKey, newValue);
                }
            }
            else if (newValue != pair.Value)
            {
                miniGameManager.GetMiniGames()[pair.Key] = newValue;
            }
        }

        if (GUILayout.Button("Add item"))
        {
            // it is necessary to create a new object here, because unity doesn't accept a gameobject that is null.
            miniGameManager.GetMiniGames().Add(CreateMiniGameCube(), null);
        }

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }
    }
    
    private GameObject CreateMiniGameCube()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "Minigame";
        return cube;
    }
}