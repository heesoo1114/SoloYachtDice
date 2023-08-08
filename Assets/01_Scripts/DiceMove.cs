using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMove : MonoBehaviour
{
    private Rigidbody rb;

    public bool isGround = false;

    public float speed = 10;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Start()
    {
        rb.AddTorque(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveCube();
        }
    }

    void MoveCube()
    {
        if (isGround)
        {
            isGround = false;
            rb.AddTorque(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
            rb.AddForce(transform.up * speed);
            rb.velocity = Vector3.up * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGround = true;
        }
    }

}