using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Minigame", menuName = "Minigame")]
public class Minigame : ScriptableObject
{
   [field: SerializeField] public string Name { get; set; }
   [field: SerializeField] public double Longitude { get; set; }
   [field: SerializeField] public double Latitude { get; set; }
   [field: SerializeField] public string SceneName { get; set; }
   [field: SerializeField] public float Radius { get; set; }
}
