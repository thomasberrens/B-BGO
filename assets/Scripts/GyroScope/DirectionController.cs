using System;
using UnityEngine;

    public class DirectionController : MonoBehaviour
    {
        public float CurrentHeading { get; private set; }
        
        public CardinalDirection CurrentDirection { get; private set; }

        private bool initialized = false;
        
        
        private void Start()
        {
            DeviceManager.Instance.OnLocationStatusChanged += OnLocationStatusChanged;
        }
        
        private void OnLocationStatusChanged(LocationServiceStatus status) => initialized = status == LocationServiceStatus.Running;
        
        void Update(){

            if (!initialized) return;

            float heading = DeviceManager.Instance.Compass.trueHeading;
            
            CurrentHeading = heading;
            
            Debug.Log("Heading: " + heading);

            CardinalDirection currentDirection = GetCardinalDirection(heading);
            
            Debug.Log("Direction: " + currentDirection);

            CurrentDirection = currentDirection;
        }
        
        private CardinalDirection GetCardinalDirection(float heading)
        {
            if (heading >= 337.5f || heading < 22.5f)
            {
                return CardinalDirection.NORTH;
            }
            else if (heading >= 22.5f && heading < 67.5f)
            {
                return CardinalDirection.NORTH_EAST;
            }
            else if (heading >= 67.5f && heading < 112.5f)
            {
                return CardinalDirection.EAST;
            }
            else if (heading >= 112.5f && heading < 157.5f)
            {
                return CardinalDirection.SOUTH_EAST;
            }
            else if (heading >= 157.5f && heading < 202.5f)
            {
                return CardinalDirection.SOUTH;
            }
            else if (heading >= 202.5f && heading < 247.5f)
            {
                return CardinalDirection.SOUTH_WEST;
            }
            else if (heading >= 247.5f && heading < 292.5f)
            {
                return CardinalDirection.WEST;
            }
            else
            {
                return CardinalDirection.NORTH_WEST;
            }
        }
    }
