using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Highlighter : MonoBehaviour {

    public Color normallyColor = Color.white;
    public Color rayHoverColor;
    bool triggered;
    public bool quit = true;
    public string level;
    public GameObject gb;

    TextMeshPro textmesh;

    void Start()
    {
        triggered = false;

        textmesh = GetComponent<TextMeshPro>();
        textmesh.color = normallyColor;

    }
    private void Update()
    {
        if (/*Input.GetKeyDown(KeyCode.A)*/gb.GetComponent<LaserPointer>().check && triggered)
        {
            SceneManager.LoadScene(level);
        }
    }

    void OnTriggerEnter(Collider other) {
        textmesh.color = rayHoverColor;
        triggered = true;
        
    }
    void OnTriggerExit() {
        textmesh.color = normallyColor;
        triggered = false;
        
    }
    void OnMouseDown() { if (quit) Debug.Log("Quit"); }
}
