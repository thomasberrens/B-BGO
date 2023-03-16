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
    //[field: SerializeField] private GameObject treePrefab { get; set; }
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Get required MapsService component on this GameObject.
        MapsService mapsService = GetComponent<MapsService>();

        // Set real-world location to load.
        mapsService.InitFloatingOrigin(LatLng);
        

        
        
        mapsService.Events.ExtrudedStructureEvents.WillCreate.AddListener(WillCreateExtrudedStructure);
        
        mapsService.Events.SegmentEvents.WillCreate.AddListener(BeforeCreateRoad);
     //   mapsService.Events.SegmentEvents.DidCreate.AddListener(OnCreateRoad);

        mapsService.Events.MapEvents.Loaded.AddListener(LoadedMap);
        
        // Load map with default options.
        mapsService.LoadMap(new Bounds(Vector3.zero, new Vector3(715, 0, 715)), ExampleDefaults.DefaultGameObjectOptions);
        
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
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
