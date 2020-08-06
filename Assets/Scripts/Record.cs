using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Record : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public int spawnRate = 50;
    public int spawnCounter = 0;

    public GameObject spawnZone;
    public GameObject obstacle;
    public static Record instance;
    private AudioSource audioSource;


    private void Awake()
    {
        if (instance == null) 
            instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // rotation mechanics 
        transform.Rotate(new Vector3(0, rotationSpeed, 0), Space.World);

        // obstacle mechanics 
        if (spawnCounter >= spawnRate) {
            Instantiate(obstacle, spawnZone.transform.position, Quaternion.identity, transform);
            spawnCounter = 0;
        }

        spawnCounter++;
    }

    public void Reverse() 
    {
        rotationSpeed *= -1.0f;
        audioSource.pitch *= -1.0f;
    }
}
