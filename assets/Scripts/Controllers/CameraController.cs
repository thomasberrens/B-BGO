using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Compass _compass;
    [SerializeField] private GameObject pivotPoint;
    [SerializeField] private Vector3 positionOffset = new Vector3(0,1,0);
    
    // Start is called before the first frame update
    void Start()
    {
        //float heading = _compass.trueHeading;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivotPoint.transform.position, positionOffset, _compass.trueHeading);
    }
}
