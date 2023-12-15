using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneDetectionController : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    //[SerializeField] private GameObject objectToSpawnPrefab;
    [SerializeField] private Button detectPlaneButton;

    private bool isPlaneDetected = false;
    [SerializeField] private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Start()
    {
        detectPlaneButton.onClick.AddListener(StartPlaneDetection);
    }

    private void StartPlaneDetection()
    {
        // Start the plane detection process
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), new List<ARRaycastHit>(), TrackableType.PlaneWithinPolygon);
    }

    private void Update()
    {
        // Check for a single plane and instantiate objects on it
        if (!isPlaneDetected && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            if (raycastManager.Raycast(touch.position, new List<ARRaycastHit>(), TrackableType.PlaneWithinPolygon))
            {
                isPlaneDetected = true;
                SpawnObjectsOnDetectedPlane();
            }
        }
    }

    private void SpawnObjectsOnDetectedPlane()
    {
        // Create a list to store raycast hits
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        // Perform a raycast and check if it hits a plane
        if (raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            // Get the first hit from the list
            ARRaycastHit hit = hits[0];

            // Spawn the first object at the hit position
            Vector3 firstObjectPosition = hit.pose.position;
            GameObject firstObject = Instantiate(spawnedObjects[0], firstObjectPosition, hit.pose.rotation);
            firstObject.AddComponent<TouchHandler>(); // Attach TouchHandler script to handle touch input
            spawnedObjects.Add(firstObject);

            // Calculate the position for the second object with a distance of 0.5 on the X-axis
            Vector3 secondObjectPosition = new Vector3(firstObjectPosition.x + 0.5f, firstObjectPosition.y, firstObjectPosition.z);
            GameObject secondObject = Instantiate(spawnedObjects[1], secondObjectPosition, hit.pose.rotation);
            secondObject.AddComponent<TouchHandler>(); // Attach TouchHandler script to handle touch input
            spawnedObjects.Add(secondObject);

            // Stop plane detection
            raycastManager.enabled = false;
        }
    }
}
