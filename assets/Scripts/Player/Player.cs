using UnityEngine;
[RequireComponent(typeof(GpsTracker)), RequireComponent(typeof(DirectionController))]
public class Player : MonoBehaviour
{
    public GpsTracker GpsTracker { get; private set; }
    public DirectionController DirectionController { get; private set; }
        
    private void Awake()
    {
        GpsTracker = GetComponent<GpsTracker>();
        DirectionController = GetComponent<DirectionController>();
    }
    
    
        
        
}