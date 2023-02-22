using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MinigameManager))]
public class SerializableDictionaryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MinigameManager miniGameManager = (MinigameManager)target;

        var dictionary = new Dictionary<GameObject, Minigame>(miniGameManager.GetMiniGames());

        EditorGUI.BeginChangeCheck();

        foreach (var pair in dictionary)
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
            miniGameManager.GetMiniGames().Add(CreateMiniGameCube(), CreateInstance<Minigame>());
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