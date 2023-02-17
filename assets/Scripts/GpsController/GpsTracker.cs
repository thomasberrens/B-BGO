using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GpsTracker : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 startGPSLocation;
    private Vector3 currentGPSLocation;

    private LocationService locationService; 
    
    [SerializeField] private bool spoofLocation = false;
    
    // first latitude, then longitude
    [SerializeField] private Vector2 spoofLocationCoordinates = new Vector2(0, 0);

    private void Start()
    {
        // Get the object's starting position
        startPosition = transform.position;

        locationService = DeviceManager.Instance.LocationService;
        
    }

    private bool initialized = false;

    private void Update()
    {
        bool isRunning = locationService.status == LocationServiceStatus.Running;
        
        if (!initialized && isRunning)
        {
            Debug.Log("Initialized.");
            startGPSLocation =  GetCartesianFromGPS(new Vector2(locationService.lastData.latitude, locationService.lastData.longitude));
            initialized = true;
            
            spoofLocationCoordinates.x = locationService.lastData.latitude;
            spoofLocationCoordinates.y = locationService.lastData.longitude;
            
            return;
        } else if (!initialized) return;
        
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
            Debug.Log("Updating location.");
            currentGPSLocation = GetCartesianFromGPS(new Vector2(latitude, longitude));

            // Calculate the offset between the GPS starting location and the current location
            Vector3 locationOffset = currentGPSLocation - startGPSLocation;


            Debug.Log("Offset to add: " + locationOffset.x + ", " + locationOffset.y + "," + locationOffset.z);
            
            // Set the object's position to the starting position plus the location offset
            transform.position = startPosition + locationOffset;
        }
    }
    
    private const float EarthRadius = 6371000f;

    private Vector3 GetCartesianFromGPS(Vector2 gpsLocation)
    {
        float latitude = gpsLocation.x;
        float longitude = gpsLocation.y;

        float x = (EarthRadius + 0f) * Mathf.Cos(latitude * Mathf.Deg2Rad) * Mathf.Cos(longitude * Mathf.Deg2Rad);
        float y = (EarthRadius + 0f) * Mathf.Cos(latitude * Mathf.Deg2Rad) * Mathf.Sin(longitude * Mathf.Deg2Rad);
        float z = (EarthRadius + 0f) * Mathf.Sin(latitude * Mathf.Deg2Rad);

        return new Vector3(x, 0, y);
    }
}
