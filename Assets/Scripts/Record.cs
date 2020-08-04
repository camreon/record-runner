using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public int spawnRate = 50;
    public int spawnCounter = 0;
    public GameObject spawnZone;
    public GameObject obstacle;

    void Start()
    {

    }

    void Update()
    {
        // rotation mechanics 
        transform.Rotate(new Vector3(0, rotationSpeed, 0), Space.World);
   
        if (Input.GetButtonDown("Fire1")) {
            rotationSpeed = rotationSpeed * -1.0f;
        }

        // obstacle mechanics 
        if (spawnCounter >= spawnRate) {
            Instantiate(obstacle, spawnZone.transform.position, Quaternion.identity, transform);
            spawnCounter = 0;
        }

        spawnCounter++;
    }
}
