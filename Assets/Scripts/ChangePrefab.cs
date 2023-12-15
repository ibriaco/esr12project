using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangePrefab : MonoBehaviour
{
    private Canvas canvas;
    private Text fact;
    private GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindAnyObjectByType<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPrefab(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
