using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameObject[] gameObjects;
    public PlacementManager placementManager;

    // Start is called before the first frame update
    void Start()
    {
        placementManager = FindAnyObjectByType<PlacementManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
