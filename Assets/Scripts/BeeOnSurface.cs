using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeOnSurface : MonoBehaviour
{
    public GameObject planet;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Spawn bee. Planet radius: " + planet.GetComponent<Planet>().Radius);
        transform.position = transform.forward * planet.GetComponent<Planet>().Radius;
    }
}
