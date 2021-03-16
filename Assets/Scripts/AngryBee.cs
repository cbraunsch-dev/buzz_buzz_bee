using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBee : MonoBehaviour
{
    public GameObject planet;
    public GameObject targetBee;
    public float speed = 5.0f;

    private Transform currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.FindGameObjectWithTag(Tags.Ground);
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeToSelectNewTarget())
        {
            // Pick a random flower to target
            var randomFlowerIndex = Random.Range(0, planet.GetComponent<Planet>().Flowers.Count);
            var targetFlower = planet.GetComponent<Planet>().Flowers[randomFlowerIndex];
            currentTarget = targetFlower.transform;
        }

        if(currentTarget != null)
        {
            var targetRotation = Quaternion.LookRotation(currentTarget.position - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }

    private bool TimeToSelectNewTarget()
    {
        // Select a new target if we currently don't have one
        if(currentTarget == null)
        {
            return true;
        }

        // Select a new target if we have reached our current target
        var targetRotation = Quaternion.LookRotation(currentTarget.position - transform.position);
        return transform.rotation == targetRotation;
    }
}
