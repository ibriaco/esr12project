/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    private ARTapToPlaceObject aRTapToPlaceObject;
    public GameObject objectToPlace;
    private Canvas canvas;

    private GameObject newPlacedObject;

    // Start is called before the first frame update
    void Start()
    {
        aRTapToPlaceObject = FindObjectOfType<ARTapToPlaceObject>();
        canvas = FindObjectOfType<Canvas>();
    }

    public void ClickToPlace()
    {
        newPlacedObject = Instantiate(objectToPlace, aRTapToPlaceObject.transform.position, aRTapToPlaceObject.transform.rotation);
    }
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementManager : MonoBehaviour
{
    private ARTapToPlaceObject aRTapToPlaceObject;
    private Canvas canvas;

    private GameObject newPlacedObject;
    private Button startButton;
    private Button firstSelectionButton;
    private Button secondSelectionButton;

    // Start is called before the first frame update
    void Start()
    {
        aRTapToPlaceObject = FindObjectOfType<ARTapToPlaceObject>();
        canvas = FindObjectOfType<Canvas>();

        // Assuming your FirstSelection and SecondSelection buttons are direct children of the Canvas.
        firstSelectionButton = canvas.transform.Find("FirstSelection").GetComponent<Button>();
        secondSelectionButton = canvas.transform.Find("SecondSelection").GetComponent<Button>();
        startButton = canvas.transform.Find("Start").GetComponent<Button>();
    }

    public void ClickToPlace()
    {
        // Deactivate the button you are clicking.
        startButton.gameObject.SetActive(false);

        // Activate the FirstSelection and SecondSelection buttons.
        firstSelectionButton.gameObject.SetActive(true);
        secondSelectionButton.gameObject.SetActive(true);

        //newPlacedObject = Instantiate(objectToPlace, aRTapToPlaceObject.transform.position, aRTapToPlaceObject.transform.rotation);
    }
}

