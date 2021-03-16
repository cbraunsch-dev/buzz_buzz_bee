using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBee : MonoBehaviour
{
    public GameObject planet;
    public GameObject targetBee;
    public float speed = 5.0f;
    public float distanceWhenToFindNewFlower = 0.5f;
    public float distanceWhenToChasePlayer = 2.0f;

    private bool chasingPlayer;
    private float timeToSpendChasingPlayer = 5.0f;
    private float timeSpentChasingPlayer = 0.0f;
    private Transform currentTarget;
    private Transform targetFlower;
    private BeeOnSurface beeOnSurface;

    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.FindGameObjectWithTag(Tags.Ground);
        beeOnSurface = transform.Find("BeeOnSurface").GetComponent<BeeOnSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        if(chasingPlayer)
        {
            timeSpentChasingPlayer += Time.deltaTime;
            if(timeSpentChasingPlayer > timeToSpendChasingPlayer)
            {
                chasingPlayer = false;
                Debug.Log("Stop chasing player");
            }
        }
        else
        {
            if(TimeToChasePlayer())
            {
                Debug.Log("Start chasing player");
                targetFlower = null;
                chasingPlayer = true;
                timeSpentChasingPlayer = 0.0f;
                currentTarget = targetBee.transform;
            }
            else if(TimeToSelectFlowerTarget())
            {
                Debug.Log("Find a new flower");
                targetFlower = PickRandomFlower();
                currentTarget = targetFlower;
            }
        }

        if(currentTarget != null)
        {
            var targetRotation = Quaternion.LookRotation(currentTarget.position - transform.position);

            // Smoothly rotate towards the target point.
            var currentProjectedPointOnSurface = transform.forward * planet.GetComponent<Planet>().Radius;
            var targetProjectedPointOnSurface = currentTarget.position;


            transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, targetRotation, (1 / Vector3.Distance(currentProjectedPointOnSurface, targetProjectedPointOnSurface)) * 0.1f);
        }
    }

    private bool TimeToChasePlayer()
    {
        // return Vector3.Distance(beeOnSurface.transform.position, targetBee.transform.position) < distanceWhenToChasePlayer;
        return false;
    }

    private Transform PickRandomFlower()
    {
        // Pick a random flower to target
        var randomFlowerIndex = Random.Range(0, planet.GetComponent<Planet>().Flowers.Count);
        var targetFlower = planet.GetComponent<Planet>().Flowers[randomFlowerIndex];
        return targetFlower.transform;
    }

    private bool TimeToSelectFlowerTarget()
    {
        // Select a new target if we currently don't have one
        if(targetFlower == null)
        {
            return true;
        }

        // Select a new target if we have reached our current target or are close enough to our current target
        return Vector3.Distance(beeOnSurface.transform.position, targetFlower.position) < distanceWhenToFindNewFlower;
    }
}
