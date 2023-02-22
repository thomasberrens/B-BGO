using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MinigameManager : MonoBehaviour{ 
       [SerializeField] private SerializableDictionary<GameObject, Minigame> miniGames = new SerializableDictionary<GameObject, Minigame>();
       
       public SerializableDictionary<GameObject, Minigame> GetMiniGames(){
              return miniGames;
       }

       private void Start()
       {
              foreach (KeyValuePair<GameObject,Minigame> entrySet in miniGames)
              {
                     GameObject gameObject = entrySet.Key;
                     Minigame minigame = entrySet.Value;
                     
                     Debug.Log("Key: " + gameObject.name + " Value: " + minigame.name);
              }
       }
}
