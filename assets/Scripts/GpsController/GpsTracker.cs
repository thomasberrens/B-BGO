using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GpsTracker : MonoBehaviour
{
     private LocationInfo previousLocation;
    private LocationInfo currentLocation;

    private const double EarthRadius = 6371000; // meters

    private Quaternion referenceRotation;
    private Vector3 referenceMagneticField;

    private Vector3 velocity;
    private Vector3 position;
    private Vector3 startPosition;

    // Maximum distance (in meters) between current and previous GPS locations
    // before we reset the object's position to the current location
    private const float MaxDistanceThreshold = 10f;

    // Maximum angle (in degrees) between current and previous GPS headings
    // before we reset the gyroscope reference rotation
    private const float MaxHeadingAngleThreshold = 10f;

    private bool initialized;
    void Start()
    {
        Input.location.Start();
    }

    void initialize()
    {
        Debug.Log("Initializing!");
        
        previousLocation = Input.location.lastData;
        currentLocation = previousLocation;

        Input.compass.enabled = true;
        Input.gyro.enabled = true;

        // Calibrate the gyroscope by taking the average of the first 30 samples
        int numSamples = 30;
        Quaternion rotationSum = Quaternion.identity;
        for (int i = 0; i < numSamples; i++)
        {
            rotationSum *= Input.gyro.attitude;
            System.Threading.Thread.Sleep(20);
        }
        
        referenceRotation = Quaternion.Lerp(referenceRotation, rotationSum, 1.0f / numSamples);
        referenceMagneticField = Input.compass.rawVector;

        // Save the starting position of the object
        startPosition = transform.position;

        // Calculate the start location offset based on the GPS location
        Vector3 startLocationCartesian = GetCartesianFromGPS(currentLocation.latitude, currentLocation.longitude) - GetCartesianFromGPS(previousLocation.latitude, previousLocation.longitude);
        position = startPosition - startLocationCartesian;
    }

    private Quaternion Divide(Quaternion quaternion, float value)
    {
        Quaternion dividedQuaternion = new Quaternion();
        
        
        dividedQuaternion.x = quaternion.x / value;
        dividedQuaternion.y = quaternion.y / value;
        dividedQuaternion.z = quaternion.z / value;
        
        return dividedQuaternion;
    }

    void Update()
    {
        
        Debug.Log("update! " + Input.location.status);
        
        if (!initialized && Input.location.status == LocationServiceStatus.Running)
        {
            initialized = true;
            initialize();
        } else return;
        
        
        if (Input.location.status == LocationServiceStatus.Running)
        {
            previousLocation = currentLocation;
            currentLocation = Input.location.lastData;

            // Check if the current location differs too much from the previous one
            if ((previousLocation.latitude - currentLocation.latitude) * (previousLocation.latitude - currentLocation.latitude) + 
                (previousLocation.longitude - currentLocation.longitude) * (previousLocation.longitude - currentLocation.longitude) > MaxDistanceThreshold * MaxDistanceThreshold)
            {
                // Reset the position to the current location
                position = startPosition - GetCartesianFromGPS(currentLocation.latitude, currentLocation.longitude);
                velocity = Vector3.zero;
            }
            else
            {
                // Convert previous location to Cartesian coordinates
                Vector3 previousLocationCartesian = GetCartesianFromGPS(previousLocation.latitude, previousLocation.longitude);

                // Convert current location to Cartesian coordinates
                Vector3 currentLocationCartesian = GetCartesianFromGPS(currentLocation.latitude, currentLocation.longitude);

                // Calculate the direction vector
                Vector3 direction = currentLocationCartesian - previousLocationCartesian;

                // Get the gyroscope angular velocity and acceleration
                Vector3 angularVelocity = Input.gyro.rotationRateUnbiased;
                Vector3 acceleration = Input.gyro.userAcceleration;

                // Convert the acceleration and angular velocity to world space
                Vector3 accelerationWorld = transform.rotation * acceleration;
                Vector3 angularVelocityWorld = referenceRotation * transform.rotation * angularVelocity;

                // Apply the gyroscope measurements
                
                // Apply the gyroscope measurements to the reference rotation and magnetic field
                referenceRotation = Quaternion.AngleAxis(angularVelocityWorld.magnitude * Time.deltaTime, angularVelocityWorld) * referenceRotation;
                referenceMagneticField = Quaternion.Inverse(referenceRotation) * Input.compass.rawVector;

                // Get the heading angle from the compass and adjust for magnetic declination
                float declinationAngle = Input.compass.trueHeading - Input.compass.magneticHeading;
                Quaternion compassRotation = Quaternion.LookRotation(referenceMagneticField);
                Vector3 headingAngle = compassRotation * Quaternion.AngleAxis(declinationAngle, Vector3.up) * Vector3.forward;

                // Calculate the direction of movement based on the GPS and compass data
                Vector3 movementDirection;
                if (direction.sqrMagnitude > 0.01f)
                {
                    movementDirection = Quaternion.LookRotation(direction) * headingAngle;
                }
                else
                {
                    movementDirection = headingAngle;
                }

                // Update the velocity and position based on the acceleration and movement direction
                velocity += accelerationWorld * Time.deltaTime;
                position += velocity * Time.deltaTime + 0.5f * accelerationWorld * Time.deltaTime * Time.deltaTime + movementDirection * direction.magnitude;

                // Reset the velocity to zero
                velocity = Vector3.zero;

                // Check if the gyroscope reference rotation needs to be reset
                float headingAngleDiff = Quaternion.Angle(referenceRotation, Input.gyro.attitude);
                if (headingAngleDiff > MaxHeadingAngleThreshold)
                {
                    referenceRotation = Input.gyro.attitude;
                }

                // Set the object's position to the calculated position plus the start position offset
                transform.position = position + startPosition;
            }
        }
    }

    private Vector3 GetCartesianFromGPS(double latitude, double longitude)
    {
        double latRad = latitude * Mathf.Deg2Rad;
        double longRad = longitude * Mathf.Deg2Rad;

        float x = (float)(EarthRadius * Math.Cos(latRad) * Math.Cos(longRad));
        float y = (float)(EarthRadius * Math.Sin(latRad));
        float z = (float)(EarthRadius * Math.Cos(latRad) * Math.Sin(longRad));

        return new Vector3(x, y, z);
    }
}
