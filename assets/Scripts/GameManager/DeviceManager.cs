using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class DeviceManager {
        
    // getter and setter for the device manager instance

    private static DeviceManager _instance;
    
    public static DeviceManager Instance
    {
        get { return _instance ??= new DeviceManager(); }
    }
    
    public Gyroscope Gyroscope { get; private set; }
    
    public Compass Compass { get; private set; }
    
    public LocationService LocationService { get; private set; }
    
    public LocationServiceStatus CurrentStatus { get; private set; }

    public event Action<LocationServiceStatus> OnLocationStatusChanged;

    public void Initialize()
    {
        Gyroscope = Input.gyro;
        Compass = Input.compass;
        LocationService = Input.location;
        
        // Start the location service, this has to be called before enabling the compass.
        LocationService.Start();
        
        Gyroscope.enabled = true;
        Compass.enabled = true;

        Debug.Log("Device Manager Initialized");

        GameManager.Instance.StartCoroutine(CheckForLocationServiceStatus());
    }

    private IEnumerator CheckForLocationServiceStatus()
    {
        while (true)
        {
            if (LocationService.status == CurrentStatus)
            {
                // yield return null is necessary to not freeze the game thread
                yield return null;
                continue;
            }

            CurrentStatus = LocationService.status;
            OnLocationStatusChanged?.Invoke(CurrentStatus);
            yield return null;
        }
    }
    
    
    

}
