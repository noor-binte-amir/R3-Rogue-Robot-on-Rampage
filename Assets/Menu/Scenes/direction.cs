using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class direction : MonoBehaviour {

    public Transform target;
    public Transform actual;

    // Use this for initialization
    void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {

        target.rotation = actual.rotation;

    }
}
