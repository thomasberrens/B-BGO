using UnityEngine;

public class LocationUtil {
    
    private const float earthRadius = 6371000f;
    
    public static Vector3 GetCartesianFromGPS(Vector2 gpsLocation)
    {
        float latitude = gpsLocation.x;
        float longitude = gpsLocation.y;

        float x = (earthRadius + 0f) * Mathf.Cos(latitude * Mathf.Deg2Rad) * Mathf.Cos(longitude * Mathf.Deg2Rad);
        float y = (earthRadius + 0f) * Mathf.Cos(latitude * Mathf.Deg2Rad) * Mathf.Sin(longitude * Mathf.Deg2Rad);

        return new Vector3(x, 0, y);
    }
    
    public static Vector3 CalculateCartesianOffset(Vector3 startPosition, Vector3 startGPSLocation, Vector3 currentGPSLocation)
    {
        Vector3 offset = currentGPSLocation - startGPSLocation;
        return startPosition + offset;
    }
}
