using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class TouchHandler : MonoBehaviour
{
    private ARCameraManager arCameraManager;
    private GameObject factObject;

    void Start()
    {
        arCameraManager = FindObjectOfType<ARCameraManager>();
        factObject = GameObject.Find("Fact"); // Make sure the name is correct
    }

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch is on this object
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch detected!");

                if (arCameraManager != null)
                {
                    Ray ray = arCameraManager.GetComponent<Camera>().ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    // Raycast to check if the touch hits the Fact object
                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == factObject)
                    {
                        Debug.Log("Raycast hit the Fact object!");

                        // Check if the name of the Fact object is contained in the text
                        Text textComponent = factObject.GetComponentInChildren<Text>();
                        if (textComponent != null)
                        {
                            string factText = textComponent.text;

                            if (factText.Contains(factObject.name))
                            {
                                Debug.Log("Name is contained in the text!");

                                // Touch hit the Fact object, make it disappear
                                Destroy(factObject);
                            }
                            else
                            {
                                Debug.Log("Name is not contained in the text!");
                            }
                        }
                        else
                        {
                            Debug.Log("The Fact object does not have a Text component!");
                        }
                    }
                    else
                    {
                        Debug.Log("Raycast did not hit the Fact object!");
                    }
                }
                else
                {
                    Debug.LogError("ARCameraManager not found!");
                }
            }
        }
    }
}
