using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spherecollision : MonoBehaviour
{

    public GameObject gb;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 90 * Time.deltaTime);
        //healthslider.value = calculatehealth();
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Player")
        {


            this.gameObject.SetActive(false);

        }


    }
}
