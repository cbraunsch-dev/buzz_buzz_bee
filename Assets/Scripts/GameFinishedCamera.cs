using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinishedCamera : MonoBehaviour
{
    public GameObject planet;

    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.FindGameObjectWithTag(Tags.Ground);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(planet.transform);
        transform.Translate(Vector3.right * Time.deltaTime);
    }
}
