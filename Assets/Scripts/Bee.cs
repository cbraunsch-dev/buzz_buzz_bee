using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public GameObject planet;
    public float acceleration;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Make the up vector always face away from the center of the planet
        transform.up = -(planet.transform.position - transform.position).normalized;

        // Give the bee a bit of a forward push
        rb.AddForce(transform.forward * 300.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Apply gravity
        rb.AddForce((planet.transform.position - transform.position).normalized * acceleration);

        // If the user hits the space bar, apply an extra downward boost
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Slam it!");
            rb.AddForce((planet.transform.position - transform.position).normalized * 500.0f);
        }

        // Make the up vector always face away from the center of the planet
        transform.up = -(planet.transform.position - transform.position).normalized;
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
