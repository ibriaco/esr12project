using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SingleObjectPlacement : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public GameObject objectToPlace;
    private bool placingObject = false;

    // Start is called before the first frame update
    void Start()
    {
        // Disable object placement at the start
        objectToPlace.SetActive(false);
    }

    public void OnStartButtonClick()
    {
        if (!placingObject)
        {
            // Enable object placement mode
            objectToPlace.SetActive(true);
            placingObject = true;

            // Disable plane detection
            planeManager.enabled = false;
        }
        else
        {
            // Place the object on the nearest detected plane
            PlaceObjectOnPlane();
        }
    }

    private void PlaceObjectOnPlane()
    {
        // Raycast from the center of the screen to find the plane
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Place the object at the hit point
            objectToPlace.transform.position = hit.point;
            objectToPlace.transform.rotation = Quaternion.identity;

            // Disable object placement mode
            placingObject = false;
            objectToPlace.SetActive(false);

            // Re-enable plane detection
            planeManager.enabled = true;
        }
    }
}
