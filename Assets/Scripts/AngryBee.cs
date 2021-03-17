using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBee : MonoBehaviour
{
    public GameObject planet;
    public GameObject targetBee;
    public float speed = 20.0f;
    public float distanceWhenToFindNewFlower = 0.5f;
    public float distanceWhenToChasePlayer = 2.0f;
    public GameObject testCubePrefab;
    public float timeBeforeChangePlayerTarget = 0.5f;

    private float timeSinceLastChangedPlayerTarget = 0.0f;

    private bool chasingPlayer = true;
    private float timeToSpendChasingPlayer = 5.0f;
    private float timeSpentChasingPlayer = 0.0f;
    private Vector3 currentTargetPosition = Vector3.zero;
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
            /*timeSpentChasingPlayer += Time.deltaTime;
            if(timeSpentChasingPlayer > timeToSpendChasingPlayer)
            {
                chasingPlayer = false;
                Debug.Log("Stop chasing player");
            }*/

            FollowPlayer();
        }
       /* else
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
        }*/

        /*if(currentTarget != null)
        {
            var targetRotation = Quaternion.LookRotation(currentTarget.position - transform.position);

            // Smoothly rotate towards the target point.
            var currentProjectedPointOnSurface = transform.forward * planet.GetComponent<Planet>().Radius;
            var targetProjectedPointOnSurface = currentTarget.position;

            // Moves bee without slowing it down as it approaches its destination
            transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, targetRotation, (1 / Vector3.Distance(currentProjectedPointOnSurface, targetProjectedPointOnSurface)) * 0.1f);
        }*/
    }

    private void FollowPlayer()
    {
        timeSinceLastChangedPlayerTarget += Time.deltaTime;
        if(timeSinceLastChangedPlayerTarget > timeBeforeChangePlayerTarget)
        {
            timeSinceLastChangedPlayerTarget = 0.0f;
            Debug.Log("Find new position near player");
            currentTargetPosition = FindNewTargetNearPlayer();
        }

        if(currentTargetPosition != Vector3.zero)
        {
            beeOnSurface.TargetPosition = targetBee.transform.position;

            var targetRotation = Quaternion.LookRotation(currentTargetPosition - transform.position);

            // Smoothly rotate towards the target point.      
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

            // Smoothly rotate towards the target point.
            var currentProjectedPointOnSurface = transform.forward * planet.GetComponent<Planet>().Radius;
            var targetProjectedPointOnSurface = currentTargetPosition;

            // Moves bee without slowing it down as it approaches its destination
            transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, targetRotation, (1 / Vector3.Distance(currentProjectedPointOnSurface, targetProjectedPointOnSurface)) * 0.1f);
        }
    }

    private bool ReachedPositionNearPlayer()
    {
        return currentTargetPosition == Vector3.zero || Vector3.Distance(beeOnSurface.transform.position, currentTargetPosition) < distanceWhenToFindNewFlower;
    }

    private Vector3 FindNewTargetNearPlayer()
    {
        // Add a bit of variance to the target rotation to make it interesting
        var variance = 1.0f;
        var targetPosWithVariance = new Vector3(targetBee.gameObject.transform.position.x + Random.Range(-variance, variance), targetBee.gameObject.transform.position.y + Random.Range(-variance, variance), targetBee.gameObject.transform.position.z + Random.Range(-variance, variance));
        //var testCube = Instantiate(testCubePrefab);
        //testCube.transform.position = targetPosWithVariance;
        return targetPosWithVariance;
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
