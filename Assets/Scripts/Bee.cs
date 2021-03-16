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

        MakePlayerUpright();

        // Give the bee a bit of a forward push
        rb.AddForce(transform.forward * 300.0f);
    }

    private void MakePlayerUpright()
    {
        var down = (planet.transform.position - transform.position).normalized;
        var forward = Vector3.Cross(transform.right, down);
        transform.rotation = Quaternion.LookRotation(-forward, -down);
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
        var targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= walkspeed;

        // Apply a force that attempts to reach our target velocity
        var velocityChange = (targetVelocity - rb.velocity);
        //var velocityChange = targetVelocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        // Clamp velocity
        var maxVeloc = 2.0f;
        var maxVelocX = Mathf.Clamp(rb.velocity.x, -maxVeloc, maxVeloc);
        //var maxVelocY = Mathf.Clamp(rb.velocity.y, -maxVeloc, maxVeloc);
        var maxVelocZ = Mathf.Clamp(rb.velocity.z, -maxVeloc, maxVeloc);
        //rb.velocity = new Vector3(maxVelocX, rb.velocity.y, maxVelocZ);

        MakePlayerUpright();
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
