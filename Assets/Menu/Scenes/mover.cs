using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class mover : MonoBehaviour {
    public Transform target;
    public Transform actual;
    public VRTouchpadMove gripcheck;

    public GameObject eye;

    public GameObject pointer;

    int y = 0;
    private Vector3 x;
	// Use this for initialization
	void Start () {
        //rig = GameObject.Find("Player").GetComponent<Rigidbody>();
        //rig = GameObject.Find("Player").GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {

        target.position = actual.position /*- new Vector3(0,1,0)*/;

        if (gripcheck.grip==true && y == 0)
        {
            x = actual.position;
            actual.position = new Vector3(-463, 182, 23);
            y++;
            eye.GetComponent<AtmosphericScattering>().enabled = false;
            eye.GetComponent<AtmosphericScatteringDeferred>().enabled = false;
            eye.GetComponent<PostProcessingBehaviour>().enabled = false;
            pointer.SetActive(true);
            pointer.transform.position = x;

        }
        if (gripcheck.grip==false && y != 0)
        {
            actual.position = x;
            y = 0;
            eye.GetComponent<AtmosphericScattering>().enabled = true;
            eye.GetComponent<AtmosphericScatteringDeferred>().enabled = true;
            eye.GetComponent<PostProcessingBehaviour>().enabled = true;
            pointer.SetActive(false);
        }


    }
}
