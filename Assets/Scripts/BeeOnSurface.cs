using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeOnSurface : MonoBehaviour
{
    public GameObject planet;

    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.FindGameObjectWithTag(Tags.Ground);
        transform.position = transform.forward * planet.GetComponent<Planet>().Radius;
    }

    void Update()
    {
        if(TargetPosition != null)
        {
            transform.LookAt(TargetPosition);
        }
    }

    public Vector3 TargetPosition { get; set; }
}
