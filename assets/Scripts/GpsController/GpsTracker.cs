using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GpsTracker : MonoBehaviour
{
    // TODO: these are serialized pure for debugging purposes. Remove them later.
   [field: SerializeField] public Vector3 StartPosition { get; set; }
   [field: SerializeField] public Vector3 StartGPSLocation { get; set; }
   [field: SerializeField] public Vector3 CurrentGPSLocation { get; set; }

    private LocationService locationService;
    private bool initialized = false;
    
    [SerializeField] private bool spoofLocation = false;
    
    // X = latitude, Z = longitude
    [SerializeField] private Vector2 spoofLocationCoordinates = new Vector2(0, 0);
    
    public event Action OnInitialized;

    private void Start()
    {
        // Get the object's starting position
        StartPosition = transform.position;
        
        DeviceManager.Instance.OnLocationStatusChanged += OnLocationStatusChanged;

        locationService = DeviceManager.Instance.LocationService;
        
    }
    
    private void OnLocationStatusChanged(LocationServiceStatus status)
    {
        if (status == LocationServiceStatus.Running) Initialize();
    }

    private void Initialize()
    {
        StartGPSLocation =  LocationUtil.GetCartesianFromGPS(new Vector2(locationService.lastData.latitude, locationService.lastData.longitude));
        
        spoofLocationCoordinates.x = locationService.lastData.latitude;
        spoofLocationCoordinates.y = locationService.lastData.longitude;
        
        initialized = true;

        OnInitialized?.Invoke();
        Debug.Log("Initialized GPS tracker.");
    }

    private void Update()
    {
        bool isRunning = DeviceManager.Instance.CurrentStatus == LocationServiceStatus.Running;

        if (!initialized) return;

        float latitude = locationService.lastData.latitude;
        float longitude = locationService.lastData.longitude;
        
        if (spoofLocation)
        {
            latitude = spoofLocationCoordinates.x;
            longitude = spoofLocationCoordinates.y;
        }

        // Update the GPS location
        if (isRunning)
        {
            CurrentGPSLocation = LocationUtil.GetCartesianFromGPS(new Vector2(latitude, longitude));

            // Calculate the offset between the GPS starting location and the current location
            Vector3 locationOffset = LocationUtil.CalculateCartesianOffset(StartPosition, StartGPSLocation, CurrentGPSLocation);

            Debug.Log("Offset to add: " + locationOffset.x + ", " + locationOffset.y + "," + locationOffset.z);
            
            // Set the object's position to the starting position plus the location offset
            transform.position = locationOffset;
        }
    }
}
