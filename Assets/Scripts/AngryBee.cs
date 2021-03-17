using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The AngryBee object acts as an anchor that is at the center of the planet. Attached to this anchor is the
// BeeOnSurface but it is offset by the radius of the planet. Think of the anchor at the center of the planet and an invisible
// arm with the length of the planet's radius. At the end of that arm is the BeeOnSurface. Swiveling this arm around makes the bee
// move on the surface of the planet. This is how this works.
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
