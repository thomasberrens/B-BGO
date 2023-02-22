using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Minigame", menuName = "Minigame")]
public class Minigame : ScriptableObject
{
   [field: SerializeField] private string Name { get; set; }
   [field: SerializeField] private double Longitude { get; set; }
   [field: SerializeField] private double Latitude { get; set; }
   [field: SerializeField] private string SceneName { get; set; }
}
