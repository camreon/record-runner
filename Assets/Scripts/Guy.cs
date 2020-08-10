using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Guy : MonoBehaviour
{
    public Vector3 jumpDirection = new Vector3(0.0f, 4.0f, 0.0f);
    public float jumpForce = 2.0f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f; 
    public bool isGrounded;

    Rigidbody rb;
    ParticleSystem particles;
    AudioSource jumpSound;
    SpriteRenderer spriteRenderer;

    void Start(){
        rb = GetComponent<Rigidbody>();
        particles = GetComponent<ParticleSystem>();
        jumpSound = GetComponent<AudioSource>();
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>()[0];
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle") {
            // TODO
            // rb.AddTorque(transform.forward * 100000.0f);
            spriteRenderer.flipX = !spriteRenderer.flipX;

            particles.Play();

            Record.instance.Reverse();
        }
    }

    void FixedUpdate()
    {      
        if (Input.GetButtonDown("Jump") && isGrounded) {
            isGrounded = false;
            rb.AddForce(jumpDirection * jumpForce, ForceMode.VelocityChange);
            jumpSound.Play();
        }

        if (rb.velocity.y < 0) {
            rb.velocity += jumpDirection * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } 
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.velocity += jumpDirection * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // TODO: test out 2d spin effect for collisions
        if (Input.GetButtonDown("Fire1")) {
            rb.AddTorque(transform.forward * 100000.0f);
        }
    }
}
