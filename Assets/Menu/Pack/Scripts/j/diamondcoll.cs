using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamondcoll : MonoBehaviour
{
    
    public Renderer shield1;
    public Renderer shield2;
    public Renderer shield3;
    public AudioSource gem;
    public Renderer rend;
   

    
    // Use this for initialization
    void Start()
    {
        Renderer shield1 = GetComponent<Renderer>();
        Renderer shield2 = GetComponent<Renderer>();
        Renderer shield3 = GetComponent<Renderer>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        gem = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,90 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        int r = other.GetComponent<playerscript>().points;
        if (other.name == "player")
        {

            other.GetComponent<playerscript>().points++;
            gem.Play();
            rend.enabled = false;


            Destroy(gameObject, gem.clip.length);
            
        }
        if ( r == 2)
        {
            shield1.enabled = true;

        }
        if(r==5) 
        {
            shield2.enabled = true;

        }
        if (r == 8)
        {
            shield3.enabled = true;

        }

    }
    
}
