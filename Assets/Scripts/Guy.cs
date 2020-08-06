using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Guy : MonoBehaviour
{
    public Vector3 jumpDirection;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    Rigidbody rb;
    ParticleSystem particles;

    void Start(){
        rb = GetComponent<Rigidbody>();
        particles = GetComponent<ParticleSystem>();

        jumpDirection = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle") {
            rb.AddTorque(transform.forward * 100000.0f);

            particles.Play();

            Record.instance.Reverse();
        }
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        
        // TODO: test out 2d spin effect for collisions
        if (Input.GetButtonDown("Fire1")) {
            rb.AddTorque(transform.forward * 100000.0f);
        }
    }
}
