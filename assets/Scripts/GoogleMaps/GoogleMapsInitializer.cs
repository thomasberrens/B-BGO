using System.Collections;
using System.Collections.Generic;
using Google.Maps;
using Google.Maps.Coord;
using UnityEngine;

public class GoogleMapsInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
// Create a new Google Maps API instance
        MapsService mapsService = new MapsService();

// Set the API key for the service
        mapsService.ApiKey = "AIzaSyC6MFB-9F3yhaxUbkzqyTGw0ny_lruS6cc";

// Define the center of the map
        LatLng mapCenter = new LatLng(37.7749, -122.4194);

// Define the zoom level of the map
        int zoomLevel = 15;

// Define the size of the map in pixels
        int mapWidth = Screen.width;
        int mapHeight = Screen.height;


        mapsService.MapPreviewOptions.Location = mapCenter;

        mapsService.Projection.Zoom = zoomLevel;
// Load the map and display it in the scene
        mapsService.MakeMapLoadRegion();
        
        
        

// Create a new game object at a specific latitude and longitude
        GameObject myObject = new GameObject("My Object");
        LatLng myLatLng = new LatLng(37.7749, -122.4194);
        Vector3 myPosition = mapsService.Projection.FromLatLngToVector3(myLatLng);
        myObject.transform.position = myPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
