using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHeart : MonoBehaviour {

    bool triggered;
    private GameObject Heart;
    private Rigidbody hrb;
    private GameObject building;

    public GameObject shield;

    public playerscript ps;

    public VRTouchpadMove controller;

    // Use this for initialization
    void Start () {
        triggered = false;
       
    }

    // Update is called once per frame
    private void Update()
    {

        //Debug.Log("triggered    " + triggered);
        if (controller.trigger && triggered)
        {
            //Debug.Log("P");
            ps.hasHeart = false;

            Heart = GameObject.FindGameObjectWithTag("Heart");
            hrb = Heart.GetComponent<Rigidbody>();
            building = transform.parent.gameObject;
            building.GetComponent<Destroy>().HoldsHeart = true;
            Heart.transform.parent = null;
            Heart.transform.position = transform.position;
            Heart.transform.localScale = new Vector3(1,1,1);
            hrb.isKinematic = false;
            shield.transform.position = building.transform.position;
            shield.transform.localScale = new Vector3(70f, 70f, 70f);
            Heart.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && col.transform.GetComponent<playerscript>().hasHeart)
        {
            ps = col.transform.GetComponent<playerscript>();
            triggered = true;
            Debug.Log("enter    " + triggered);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            triggered = false;

            Debug.Log("exit    " + triggered);
        }
    }
}
