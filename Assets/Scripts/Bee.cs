using System.Collections;
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
        beeModel = transform.Find("SimpleBee");

        // Place bee on surface
        transform.position = Vector3.up * planet.GetComponent<Planet>().Radius;
    }

    // Update is called once per frame
    void Update()
    {
        // Apply gravity
        rb.AddForce((planet.transform.position - transform.position).normalized * acceleration, ForceMode.Acceleration);

        var maxVelocityChange = 2.1f;
        var walkspeed = 5.0f;
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var targetVelocity = new Vector3(horizontalInput, 0, verticalInput);
        
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= walkspeed;

        // Apply a force that attempts to reach our target velocity
        var velocityChange = (targetVelocity - rb.velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        // Calculate direction bee model should face
        var angle = Mathf.Atan2(verticalInput, -horizontalInput) * Mathf.Rad2Deg;
        var down = (planet.transform.position - transform.position).normalized;
        var forward = Vector3.Cross(transform.right, down);
        transform.rotation = Quaternion.LookRotation(-forward, -down);
        if (verticalInput != 0.0f || horizontalInput != 0.0f)
        {
            beeModel.rotation = Quaternion.AngleAxis(angle, transform.up) * Quaternion.LookRotation(-forward, -down);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.RedFlower || other.tag == Tags.GreenFlower ||
            other.tag == Tags.BlueFlower || other.tag == Tags.YellowFlower)
        {
            other.gameObject.GetComponent<Flower>().ReactToCollisionWithBee();
        }
    }
}
