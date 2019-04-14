using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Destroy(gameObject, 30);
        Debug.Log("destroyed");

    }
}
