using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GpsTracker : MonoBehaviour
{
    private LocationInfo previousLocation;
    private LocationInfo currentLocation;
    private Vector3 startLocation;
    private Vector3 currentGameLocation;
    
    private const double EarthRadius = 6371000; // meters

    void Start()
    {

    }

    private bool initialized = false;

    void Update()
    {
        if (!initialized && DeviceManager.Instance.LocationService.status.Equals(LocationServiceStatus.Running))
        {
            initialized = true;
            
            startLocation = this.transform.position;
            currentGameLocation = startLocation;
            
            Debug.Log("init");
            
            previousLocation = DeviceManager.Instance.LocationService.lastData;
            currentLocation = previousLocation;
        } else if(!initialized) return;
        
        previousLocation = currentLocation;
        currentLocation = Input.location.lastData;
        
        Debug.Log("update");
        
        // Convert previous location to Cartesian coordinates
        double previousLatRad = previousLocation.latitude * Mathf.Deg2Rad;
        double previousLongRad = previousLocation.longitude * Mathf.Deg2Rad;
        Vector3 previousLocationCartesian = GetCartesianFromGPS(previousLocation.latitude, previousLocation.longitude);
            

        // Convert current location to Cartesian coordinates
        
        double currentLatRad = currentLocation.latitude * Mathf.Deg2Rad;
        double currentLongRad = currentLocation.longitude * Mathf.Deg2Rad;
        Vector3 currentLocationCartesian = GetCartesianFromGPS(currentLocation.latitude, currentLocation.longitude);
        // Calculate the direction vector
        Vector3 direction = currentLocationCartesian - previousLocationCartesian;
        direction.Normalize();
        Vector3 transformPos = transform.position;
        transformPos += direction;
        
        if(!Vector3.zero.Equals(direction)) 
            Debug.Log("DIR: " + direction.x + "," + direction.y + "," + direction.z);
        
       

        transform.position = transformPos;
    }

    private Vector3 GetCartesianFromGPS(double latitude, double longitude)
    {
        double latRad = latitude * Mathf.Deg2Rad;
        double longRad = longitude * Mathf.Deg2Rad;

        float x = (float)(EarthRadius * Math.Cos(latRad) * Math.Cos(longRad));
        float y = (float)(EarthRadius * Math.Cos(latRad) * Math.Sin(longRad));
        float z = (float)(EarthRadius * Math.Sin(latRad));

        return new Vector3(x, z, y);
    }

    private double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
    
}
