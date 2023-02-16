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

    private void Start()
    {
        // Get the object's starting position
        startPosition = transform.position;

        // Start the location service
        Input.location.Start();
    }

    private bool initialized = false;

    private float extraOffset = 0.01f;
    
    private void Update()
    {
        bool isRunning = Input.location.status == LocationServiceStatus.Running;

        if (!initialized && isRunning)
        {
            Debug.Log("Initialized.");
            startGPSLocation =  GetCartesianFromGPS(new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude));
            initialized = true;
            return;
        } else if (!initialized) return;
        
        // Update the GPS location
        if (isRunning)
        {
            Debug.Log("Updating location.");
            currentGPSLocation = GetCartesianFromGPS(new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude));

            // Calculate the offset between the GPS starting location and the current location
            Vector3 locationOffset = currentGPSLocation - startGPSLocation;


            Debug.Log("Offset to add: " + locationOffset.x + ", " + locationOffset.y);
            
            // Set the object's position to the starting position plus the location offset
            transform.position = startPosition + locationOffset;
        }
    }
    
    private const float EarthRadius = 6371000f;

    private Vector3 GetCartesianFromGPS(Vector2 gpsLocation)
    {
        float latitude = (float)gpsLocation.x;
        float longitude = (float)gpsLocation.y;

        float x = (EarthRadius + 0f) * Mathf.Cos(latitude * Mathf.Deg2Rad) * Mathf.Cos(longitude * Mathf.Deg2Rad);
        float y = (EarthRadius + 0f) * Mathf.Cos(latitude * Mathf.Deg2Rad) * Mathf.Sin(longitude * Mathf.Deg2Rad);
        float z = (EarthRadius + 0f) * Mathf.Sin(latitude * Mathf.Deg2Rad);

        return new Vector3(x, y, z);
    }
}
