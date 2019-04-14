using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyMovement : MonoBehaviour

{
    private List<Transform> list;
    private GameObject[] buildings;
    private Transform[] targets;
    private Transform target;
    private int buildingCount;
    private int count;
    public bool x = false;
    public bool fh = false;

    private Vector3 myPosition;
    private UnityEngine.AI.NavMeshAgent nav;
    private Animator anim;
    private bool triggered;
    public GameObject rhand;
    private SphereCollider SC;
    private Rigidbody rb;
    private CapsuleCollider CC;
    public int smashspeed = 20;
    public int walkBackwardSpeed = 5;
    public GameObject cage;
    private BoxCollider cagebox;
    private GameObject player;
    private bool playerHoldsHeart;
    public float spawnTime = 3f;
    private bool Dead;
    private playerscript ps;


    private bool FoundHeart;
    private GameObject Heart;
    private Rigidbody hrb;

    private int strength;
    private bool FoundShield;
    private GameObject shield;

    void Awake()
    {
        anim = GetComponent<Animator>();
        Dead = false;
        triggered = false;
        myPosition = transform.position;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        SC = GetComponent<SphereCollider>();
        CC = GetComponent<CapsuleCollider>();
        cagebox = cage.GetComponent<BoxCollider>();
        cagebox.enabled = false;

        ps = GameObject.Find("Player").GetComponent<playerscript>();

        GetBuildingTransforms();
        count = 0;
        buildingCount = FindClosest(targets);
        target = targets[buildingCount];

        FoundHeart = false;
        playerHoldsHeart = false;

        FoundShield = false;
        anim.SetBool("returnToDefault", true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || ps.win)
        {
            if (!x)
            {
                nav.isStopped = true;
                anim.SetTrigger("Dead");
                Dead = true;
                x = true;
            }
        }

        if (!Dead)
        {
            Heart = GameObject.FindGameObjectWithTag("Heart");
            if (Heart)
            {
                hrb = Heart.GetComponent<Rigidbody>();
                if (Heart.transform.parent == null)
                {
                    cagebox.enabled = true;
                }
            }
            if (!FoundShield)
            {
                shield = GameObject.FindGameObjectWithTag("Shield");
            }

            if (shield)
            {
                strength = shield.GetComponent<Disappear>().strength;
            }


            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("smash"))
            {
                triggered = true;
                nav.enabled = false;
                if (this.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .2 && this.anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= .3)
                {
                    transform.position += transform.forward * smashspeed * Time.deltaTime;
                }
                if (strength == 0)
                {
                    anim.SetBool("returnToDefault", true);
                }
            }
            else
            {
                if (playerHoldsHeart)
                {
                    target = player.transform;
                }
                if (!playerHoldsHeart && triggered && !FoundHeart && !FoundShield)
                {
                    Debug.Log("ComputeNextBuilding");
                    ComputeNextBuilding();

                }

                nav.enabled = true;
                nav.SetDestination(target.position);
            }


            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("IdleGrab_LowFront"))
            {
                Debug.Log("Grabbing");
                nav.isStopped = true;
                if (this.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .5 && this.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .6)
                {
                    bool once = true;
                    if (once)
                    {
                        Heart.layer = 12;
                        hrb.isKinematic = true;
                        Heart.transform.SetParent(rhand.transform, true);
                        Heart.transform.localPosition = new Vector3(0f, 0.0003f, 0.001f);
                        Debug.Log("Holding");
                        once = false;
                    }

                }
            }

            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("WalkBackward"))
            {
                Debug.Log("Retreating");
                nav.enabled = false;
                transform.position -= transform.forward * walkBackwardSpeed * Time.deltaTime;
                if (this.anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= .5)
                {
                    SC.enabled = false;
                }
                if (this.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .9)
                {
                    SC.enabled = true;
                    shield.tag = "Shield";
                }
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (!Dead)
        {


            if (other.CompareTag("Building"))
            {
                anim.SetBool("returnToDefault", true);
                nav.enabled = false;
                count++;

                if (other.name != "Player")
                {
                    other.transform.Find("GameObject 1").gameObject.SetActive(false);
                }
                other.tag = "Untagged";
                if (!playerHoldsHeart)
                {
                    FoundHeart = other.GetComponent<Destroy>().HoldsHeart;
                }
                else
                {
                    Debug.Log("asdfsd   " + playerHoldsHeart);
                    FoundHeart = true;
                }
                anim.SetTrigger("Hit");
                Debug.Log("Hit");
                Debug.Log("Building " + count);
                if (FoundHeart)
                {
                    SC.radius = 0.6f;
                    target = Heart.transform;
                }
                return;
            }
            if (other.CompareTag("Heart"))
            {
                GrabHeart();
                fh = true;
            }
            if (other.CompareTag("Shield"))
            {
                Debug.Log(other.tag);
                nav.enabled = false;
                shield.tag = "Untagged";
                StrikeShield();
                FoundShield = true;
            }
        }
    }

    public void ComputeNextBuilding()
    {
        list = new List<Transform>(targets);
        list.RemoveAt(buildingCount);
        targets = list.ToArray();
        Debug.Log(targets.Length);
        if (targets.Length == 0)
        {
            playerHoldsHeart = true;
            player = GameObject.FindGameObjectWithTag("Player");
            player.tag = "Building";
            player = GameObject.FindGameObjectWithTag("Building");
            Debug.Log("Building " + count);
            triggered = false;
            target = player.transform;
            return;
        }
        buildingCount = FindClosest(targets);
        target = targets[buildingCount];
        triggered = false;
        return;
    }

    
    void StrikeShield()
    {
        shield.GetComponent<Disappear>().strength = shield.GetComponent<Disappear>().strength - 1;
        anim.SetTrigger("Hit");
        if (strength != 0)
        {
            anim.SetBool("returnToDefault", false);
        }
    }

    int FindClosest(Transform[] targets)
    {
        float closestDistance = (targets[0].position - myPosition).sqrMagnitude;
        int targetNumber = 0;
        for (int i = 1; i < targets.Length; i++)
        {
            float thisDistance = (targets[i].position - myPosition).sqrMagnitude;
            if (thisDistance < closestDistance)
            {
                closestDistance = thisDistance;
                targetNumber = i;
            }
        }

        return targetNumber;
    }

    void GrabHeart()
    {
        nav.enabled = false;
        anim.SetTrigger("Grab");
    }

    void GetBuildingTransforms()
    {
        buildings = GameObject.FindGameObjectsWithTag("Building");
        targets = new Transform[buildings.Length];
        for (int i = 0; i < buildings.Length; i++)
        {
            targets[i] = buildings[i].transform;
        }
    }
}

