using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ClickObject : MonoBehaviour
{ 
    private Quaternion initialRotation;
    public AudioSource audiosource;

    public string audioname;

    public delegate void RotationEvent();
    public static event RotationEvent OnRotation;

    void Start()
    {
        // Store the initial rotation of the GameObject
        initialRotation = transform.rotation;
    }

    // Call this method to manually check for rotation
    public void CheckRotation()
    {
        // Check if the current rotation is different from the initial rotation
        if (Quaternion.Angle(transform.rotation, initialRotation) > 0.1f)
        {
            // The GameObject has been rotated
            Debug.Log("GameObject Rotated: " + gameObject.name);

            // Optionally, update the initial rotation to the current rotation
            // if you want to continue checking for further rotations
            // initialRotation = transform.rotation;

            // Trigger the rotation event
            OnRotation?.Invoke();
        }
    }
}

