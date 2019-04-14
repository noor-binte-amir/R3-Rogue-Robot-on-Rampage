using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    public GameObject robot;
    EnemyMovement em;
    public GameObject destroyedVersion;
    private Vector3 fixedPos;
    private Quaternion fixedRotation;
    public bool HoldsHeart=false;

    private void Start()
    {
        em = robot.GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("entered");
        if (other.tag == "hand")
        {
            //fixedPos = transform.position;
            //fixedPos.y = transform.position.y - 5f;

            //fixedRotation = transform.rotation;

            //fixedRotation.y = 90;
            //fixedRotation.y = 180;

            Debug.Log("hand");
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Debug.Log("fall");
            gameObject.SetActive(false);
            
        }
        
    }
    
}
