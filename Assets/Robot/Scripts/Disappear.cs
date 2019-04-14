using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    public int strength;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hand"))
        {
            Debug.Log(strength);
            if (strength == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
