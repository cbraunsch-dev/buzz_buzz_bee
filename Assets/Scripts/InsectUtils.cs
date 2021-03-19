using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InsectUtils
{
    public static Transform PickRandomFlower(Planet planet)
    {
        // Pick a random flower to target
        var randomFlowerIndex = Random.Range(0, planet.GetComponent<Planet>().Flowers.Count);
        var targetFlower = planet.GetComponent<Planet>().Flowers[randomFlowerIndex];
        return targetFlower.transform;
    }

    public static bool TimeToSelectFlowerTarget(Transform targetFlower, Vector3 bugOnSurfacePosition, float distanceWhenToFindNewFlower)
    {
        // Select a new target if we currently don't have one
        if (targetFlower == null)
        {
            return true;
        }

        // Select a new target if we have reached our current target or are close enough to our current target
        return Vector3.Distance(bugOnSurfacePosition, targetFlower.position) < distanceWhenToFindNewFlower;
    }
}
