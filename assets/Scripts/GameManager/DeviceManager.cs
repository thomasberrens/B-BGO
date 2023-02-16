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
    

    private DeviceManager()
    {
        Gyroscope = Input.gyro;
        Compass = Input.compass;
        LocationService = Input.location;
        
        
        
        // Start the location service, this has to be called before enabling the compass.
        LocationService.Start();
        
        Gyroscope.enabled = true;
        Compass.enabled = true;
        
        Debug.Log("Device Manager Initialized");
        
        
    }
    
    

}
