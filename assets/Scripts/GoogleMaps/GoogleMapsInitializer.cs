using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Google.Maps;
using Google.Maps.Coord;
using Google.Maps.Event;
using Google.Maps.Examples.Shared;
using Google.Maps.Feature;
using Google.Maps.Feature.Shape;
using Google.Maps.Feature.Style;
using UnityEngine;

public class GoogleMapsInitializer : MonoBehaviour
{
    [field: SerializeField] private LatLng LatLng { get; set; }
    [field: SerializeField] private GameObject treePrefab { get; set; }
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Get required MapsService component on this GameObject.
        MapsService mapsService = GetComponent<MapsService>();

        // Set real-world location to load.
        mapsService.InitFloatingOrigin(LatLng);
        

        
        
        mapsService.Events.ExtrudedStructureEvents.WillCreate.AddListener(WillCreateExtrudedStructure);
        
        mapsService.Events.SegmentEvents.WillCreate.AddListener(BeforeCreateRoad);
        mapsService.Events.SegmentEvents.DidCreate.AddListener(OnCreateRoad);

        mapsService.Events.MapEvents.Loaded.AddListener(LoadedMap);
        
        // Load map with default options.
        mapsService.LoadMap(ExampleDefaults.DefaultBounds, ExampleDefaults.DefaultGameObjectOptions);
        
    }

    void LoadedMap(MapLoadedArgs args)
    {

    }

    private void WillCreateExtrudedStructure(WillCreateExtrudedStructureArgs structureArgs)
    {
        
    }

    private void BeforeCreateRoad(WillCreateSegmentArgs roadArgs)
    {
     
    }

    private void OnCreateRoad(DidCreateSegmentArgs args)
    {
        Vector2 origin = args.MapFeature.Shape.Origin;
        
        Bounds bounds = args.MapFeature.Shape.BoundingBox;
        
        // Calculate the minimum and maximum X and Z coordinates of the bounding box
        float minX = bounds.min.x;
        float maxX = bounds.max.x;
        float minZ = bounds.min.z;
        float maxZ = bounds.max.z;
        
        
        // Calculate the distance from the center point to the edges of the bounding box
        float leftDistance = origin.x - minX;
        float rightDistance = maxX - origin.x;
        float bottomDistance = origin.y - minZ;
        float topDistance = maxZ - origin.y;

        
        Debug.Log("created origin: " + origin);

        int treeCount = 3;

        float treeOffset = 0.1f;
        
       List<Vector3> treePositions = new List<Vector3>();

       Vector3 left = new Vector3(origin.x + leftDistance, 0,origin.y);
       
       treePositions.Add(left);
      

// Add tree objects to the scene
        foreach (Vector3 position in treePositions) {
            GameObject tree = Instantiate(treePrefab, position, Quaternion.identity);
            tree.transform.SetParent(args.GameObject.transform);
            
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
