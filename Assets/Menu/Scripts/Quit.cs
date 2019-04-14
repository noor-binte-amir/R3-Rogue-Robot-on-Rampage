using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{

    public Color normallyColor = Color.white;
    public Color rayHoverColor;
    bool triggered;
    public bool quit = true;

    TextMeshPro textmesh;

    void Start()
    {
        triggered = false;

        textmesh = GetComponent<TextMeshPro>();
        textmesh.color = normallyColor;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && triggered)
        {
            Application.Quit();
        }
    }

    void OnTriggerEnter()
    {
        textmesh.color = rayHoverColor;
        triggered = true;
    }
    void OnTriggerExit()
    {
        textmesh.color = normallyColor;
        triggered = false;
    }
    void OnMouseDown() { if (quit) Debug.Log("Quit"); }
}
