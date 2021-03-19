using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The AngryBee object acts as an anchor that is at the center of the planet. Attached to this anchor is the
// BugOnSurface but it is offset by the radius of the planet. Think of the anchor at the center of the planet and an invisible
// arm with the length of the planet's radius. At the end of that arm is the BugOnSurface. Swiveling this arm around makes the bee
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

    
}
