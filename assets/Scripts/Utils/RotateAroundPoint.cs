using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject _pivotPoint;
    private Compass _compass;
    [SerializeField] private Vector3 _positionOffset = new Vector3(0,1,0);
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(_pivotPoint.transform.position, _positionOffset, _compass.trueHeading);
    }
}
