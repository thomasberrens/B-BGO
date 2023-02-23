using System;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour{ 
       [SerializeField] private SerializableDictionary<GameObject, Minigame> miniGames = new SerializableDictionary<GameObject, Minigame>();
       
       public SerializableDictionary<GameObject, Minigame> GetMiniGames(){
              return miniGames;
       }
       
       private void Start()
       {
              // TODO: this only works because the game is single player. If we want to support multiplayer, we need to move this to a different place.
              // in that case we should assign a minigame manager to each player.
              GameManager.Instance.Player.GpsTracker.OnInitialized += Initialize;
       }

       private void Update()
       {
              foreach (var (gameObject, miniGame) in miniGames)
              {
                    Vector3 miniGamePosition = gameObject.transform.position;
                    
                    // TODO: keep in mind that the GPS has a accuracy of 10-15 meters, so we should probably use that as a radius (with a small offset).
                    float distance = Vector3.Distance(GameManager.Instance.Player.transform.position, miniGamePosition);
                       
                    if (distance < miniGame.Radius) {
                           Debug.Log("Player has entered the radius of minigame: " + miniGame.Name);
                           
                           // TODO: trigger the minigame
                    }
              }
       }

       private void Initialize()
       {
              Vector3 startPosition = GameManager.Instance.Player.GpsTracker.StartPosition;
              Vector3 startGPSLocation = GameManager.Instance.Player.GpsTracker.StartGPSLocation;
              
              foreach (KeyValuePair<GameObject,Minigame> entrySet in miniGames)
              {
                     GameObject gameObject = entrySet.Key;
                     Minigame minigame = entrySet.Value;

                     double longitude = minigame.Longitude;
                     double latitude = minigame.Latitude;
                     
                     Vector3 gpsLocation = LocationUtil.GetCartesianFromGPS(new Vector2((float) latitude, (float) longitude));

                     Vector3 gameLocation = LocationUtil.CalculateCartesianOffset(startPosition, startGPSLocation, gpsLocation);
                     
                     gameObject.transform.position = gameLocation;
                     
                     Debug.Log("Minigame location: " + gameLocation.x + ", " + gameLocation.y + ", " + gameLocation.z);

              }
       }
}
