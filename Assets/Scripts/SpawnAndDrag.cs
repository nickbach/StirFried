using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndDrag : MonoBehaviour
{
    // Drag the prefab you would like spawned into this variable
    //  in the Unity inspector
    [SerializeField]
    GameObject objectToSpawn;

    // Reference to the instantiated object
    GameObject spawnedObject;

    // Stores the information of where our raycast hits
    RaycastHit hit;

    // The ray we're going to cast with our left click
    Ray ray;

    // The layer number for the "SpawnedObject" layer and 
    //  a layer mask that will tell the ray to ignore that layer.
    int spawnedObjectLayer, spawnedObjectMask;

    // Use this for initialization
    void Start()
    {
        // Get the number for the "SpawnedObject" layer.
        spawnedObjectLayer = LayerMask.NameToLayer("SpawnedObject");

        // Create a layer mask that ignores this layer
        spawnedObjectMask = ~(1 << spawnedObjectLayer);
    }

    // Update is called once per frame
    void Update()
    {
        // Create a ray so we can raycast it later
        ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

        // If we click/hold the left mouse
        if (Input.GetMouseButton(0))
        {
            // Raycast from our Camera to our mouse position
            // If the ray hits an object that does not have a layer of "SpawnedObject"
            if (Physics.Raycast(ray, out hit, 999999f, spawnedObjectMask))
            {
                // If we don't have a spawnedObject yet.
                if (spawnedObject == null)
                {
                    // Spawn our object at the mouse's position
                    spawnedObject = Instantiate(objectToSpawn, hit.point, Quaternion.identity);

                    // Set it's layer to "SpawnedObject" so that our raycast ignores it
                    spawnedObject.layer = spawnedObjectLayer;
                }
                // If we do have a spawnedObject
                else
                {
                    // Changes the position of our spawned object to
                    //  where the mouse's position is.
                    spawnedObject.transform.position = hit.point;
                }
            }
        }
        // If we let go of the left mouse button
        else if (Input.GetMouseButtonUp(0))
        {
            // Sets the object's layer back to "Default"
            spawnedObject.layer = 0;

            // Gets rid of our reference for the spawned object so
            //  that we can't drag it anymore.
            spawnedObject = null;
        }
    }
}
