using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterInterior : MonoBehaviour {

    //public string level;
    //GameObject old;
    //GameObject New;

    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("shift scene.");
            //SceneManager.LoadScene(level);
            /*old = GameObject.FindGameObjectWithTag("Player");
            New = GameObject.FindGameObjectWithTag("Player2");

            old.SetActive(false);
            New.SetActive(true);*/

            other.transform.position = new Vector3(-1374.19f, 1f, 9.06f);


        }
    }
}
