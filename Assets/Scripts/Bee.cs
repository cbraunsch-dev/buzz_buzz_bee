﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public GameObject planet;
    public float acceleration;
    private Rigidbody rb;
    private Transform beeModel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Place bee on surface
        transform.position = Vector3.up * planet.GetComponent<Planet>().Radius;

        beeModel = transform.Find("SimpleBee");

        MakePlayerUpright();

        // Give the bee a bit of a forward push
        rb.AddForce(transform.forward * 300.0f);
    }

    private void MakePlayerUpright()
    {
        var down = (planet.transform.position - transform.position).normalized;
        var forward = Vector3.Cross(transform.right, down);
        transform.rotation = Quaternion.LookRotation(-forward, -down);

        //beeModel.up = -down;
    }

    // Update is called once per frame
    void Update()
    {
        // Apply gravity
        rb.AddForce((planet.transform.position - transform.position).normalized * acceleration, ForceMode.Acceleration);

        // If the user hits the space bar, apply an extra downward boost
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Slam it!");
            rb.AddForce((planet.transform.position - transform.position).normalized * 500.0f);
        }

        var maxVelocityChange = 2.1f;
        var walkspeed = 5.0f;
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= walkspeed;

        // Apply a force that attempts to reach our target velocity
        var velocityChange = (targetVelocity - rb.velocity);
        //var velocityChange = targetVelocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        // Calculate direction bee model should face
        var angle = Mathf.Atan2(verticalInput, -horizontalInput) * Mathf.Rad2Deg;
        Debug.Log("Bee angle: " + angle);

        var down = (planet.transform.position - transform.position).normalized;
        var forward = Vector3.Cross(transform.right, down);
        transform.rotation = Quaternion.LookRotation(-forward, -down);
        beeModel.rotation = Quaternion.AngleAxis(angle, transform.up) * Quaternion.LookRotation(-forward, -down);

        //beeModel.forward = Quaternion.AngleAxis(angle, transform.up) * beeModel.forward;
        //Quaternion.LookRotation(Quaternion.AngleAxis(angle, transform.up) * beeModel.forward, -(planet.transform.position - transform.position).normalized);
        //beeModel.rotation = Quaternion.LookRotation(velocityChange);

        
        //MakePlayerUpright();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Give the bee a forward boost when he hits the ground
        if(collision.collider.tag == Tags.Ground)
        {
            rb.AddForce(transform.forward * 100.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.Flower)
        {
            other.gameObject.GetComponent<Flower>().ReactToCollisionWithBee(this);
        }
    }
}
