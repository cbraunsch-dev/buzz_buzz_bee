using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBee : MonoBehaviour
{
    public GameObject targetBee;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Transform PickRandomFlower(Planet planet)
    {
        // Pick a random flower to target
        var randomFlowerIndex = Random.Range(0, planet.GetComponent<Planet>().Flowers.Count);
        var targetFlower = planet.GetComponent<Planet>().Flowers[randomFlowerIndex];
        return targetFlower.transform;
    }

    public static bool TimeToSelectFlowerTarget(Transform targetFlower, Vector3 beeOnSurfacePosition, float distanceWhenToFindNewFlower)
    {
        // Select a new target if we currently don't have one
        if(targetFlower == null)
        {
            return true;
        }

        // Select a new target if we have reached our current target or are close enough to our current target
        return Vector3.Distance(beeOnSurfacePosition, targetFlower.position) < distanceWhenToFindNewFlower;
    }
}
