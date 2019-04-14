using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject Robot;
    public float spawnTime = 0f;
    public Transform[] spawnPoints;
    int spawnPointIndex;
    public bool x = false;
    public GameObject interactables;
    public GameObject enemies;

    public playerscript ps;

    // Use this for initialization
    void Start () {
        spawnPointIndex = Random.Range(0, spawnPoints.Length);
        
    }

    private void Update()
    {
        if (ps.hasHeart == false && !x) {
            Invoke("Spawn", spawnTime);
            interactables.SetActive(true);
            enemies.SetActive(true);
            x = true;
        }
    }

    // Update is called once per frame
    void Spawn () {
        Instantiate(Robot, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
