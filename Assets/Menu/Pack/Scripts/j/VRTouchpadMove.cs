using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------------------------------------------------\\
// This code has been developed by Feisty Crab Studios for personal, commercial, and education use.\\
//                                                                                                 \\
// You are free to edit and redistribute this code, subject to the following:                      \\
//                                                                                                 \\
//      1. You will not sell this code or an edited version of it.                                 \\
//      2. You will not remove the copyright messages                                              \\
//      3. You will give credit to Feisty Crab Studios if used commercially                        \\
//      4. Don't be a mean sausage, nobody likes a mean sausage.                                   \\
//                                                                                                 \\
// Contact us @ feistycrabstudios.gmail.com with any questions.                                    \\
//-------------------------------------------------------------------------------------------------\\

public class VRTouchpadMove : MonoBehaviour
{

    [SerializeField]
    GameObject rig;
    Rigidbody rb;
    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private Vector2 axis = Vector2.zero;

    int x = 1;

    public bool bound = false;

    bool jumping = false;
    public bool trigger = false;

    public bool grip = false;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        rb = rig.GetComponent<Rigidbody>();

        //rig= GameObject.Find("Player").GetComponent<Rigidbody>();

    }

    void Update()
    {
        
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }
        
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (controller.GetTouch(touchpad))
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            //Debug.Log("1");
            if (rb != null)
            {

                rb.isKinematic = false;

                //rig.position += (transform.right * axis.x + transform.forward * axis.y) * Time.deltaTime;
                //rig.position = new Vector3(rig.position.x, rig.position.y, rig.position.z);
                //Debug.Log(axis.x + "    " + axis.y);
                rb.AddRelativeForce(new Vector3(axis.x*25, 0, axis.y*25));
           
            }
        }
        else
        {
            rb.isKinematic = true;
        }
        if (controller.GetHairTriggerDown())
        {
            trigger = true;
            Debug.Log(trigger);
            if (jumping == false)
            {
                rb.AddForce(0, 300, 0);
                jumping = true;
            }

        }


        if (bound == true)
        {
            bound = false;

            SteamVR_Controller.Input((int)trackedObj.index).TriggerHapticPulse(2000);
        }

        if (controller.GetHairTriggerUp())
        {
            Debug.Log(trigger);
            trigger = false;
        }

            //Debug.Log(x % 100);
            if (jumping == true)
        {
            x++;
        }
        if (x % 30 == 0)
        {
            jumping = false;
        }

        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            grip = true;
        }
        if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            grip = false;
        }

    }
}