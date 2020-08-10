using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Record : MonoBehaviour
{
    public float score = 0;
    public Text scoreText;

    public float rotationSpeed = 1;
    public int spawnLifespan = 20;
    public int spawnRate = 200;
    private int spawnTimer = 0;

    [Range(0, 1)]
    public float spawnChance = 1f;

    public GameObject spawnZone;
    public GameObject obstacle;
    public static Record instance;
    
    private AudioSource audioSource;
    private AudioClip[] tracks = new AudioClip[2];
    public AudioClip track;
    public AudioClip reversedTrack;
    private int currentTrack = 1;

    private void Awake()
    {
        if (instance == null) 
            instance = this;

        audioSource = GetComponent<AudioSource>();

        tracks[0] = track;
        tracks[1] = reversedTrack;
    }

    void FixedUpdate()
    {
        // rotation mechanics 
        transform.Rotate(new Vector3(0, 0, rotationSpeed), Space.Self);

        rotationSpeed += score / 1000000;

        // obstacle mechanics 
        if (spawnTimer > Random.Range(spawnRate, spawnRate + 100) && spawnChance > Random.Range(0f, 1f)) {
            GameObject clone = Instantiate(obstacle, spawnZone.transform.position, Quaternion.identity, transform);
            
            Destroy(clone, spawnLifespan);

            spawnTimer = 0;
        }

        spawnTimer++;

        // score mechanics 
        score += rotationSpeed;
        scoreText.text = ((int)score / 100).ToString();
    }

    public void Reverse() 
    {
        rotationSpeed *= -1;
        
        // doesnt work with WebGL Audio API
        // audioSource.pitch *= -1;
        // instead switch to the already reversed music 
        audioSource.clip = tracks[currentTrack++ % tracks.Length];
        audioSource.Play();
    }

    public void setAudio(AudioClip audio) 
    {
        tracks[0] = audio;
        audioSource.clip = audio;
        audioSource.Play();
    }

    public void setReversedAudio(AudioClip audio) 
    {
        tracks[1] = audio;
    }
}
