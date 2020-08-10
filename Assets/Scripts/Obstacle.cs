using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Obstacle : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip spawnSound;
    public AudioClip despawnSound;
    public AudioClip hitSound;

    public Sprite[] sprites;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        
        audioSource.PlayOneShot(spawnSound);

        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Guy") {
            audioSource.PlayOneShot(hitSound, 0.4f);
        }
    }

    void OnDestroy()
    {
        // play at camera position
        AudioSource.PlayClipAtPoint(despawnSound, new Vector3(2, 30, -50), 1);
    }
}
