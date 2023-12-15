using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    private GameObject placementIndicator;
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        placementIndicator = transform.GetChild(0).gameObject;
        placementIndicator.SetActive(false);
    }

    void Update()
    {
        var ray = new Vector2(Screen.width / 2, Screen.height / 2);

        if (raycastManager.Raycast(ray, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;

            transform.position = hitPose.position;
            transform.rotation = hitPose.rotation;

            if (!placementIndicator.activeInHierarchy)
            {
                placementIndicator.SetActive(true);
            }
        }

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
    }


    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}