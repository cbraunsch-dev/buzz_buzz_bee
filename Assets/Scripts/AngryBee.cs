using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBee : MonoBehaviour
{
    public GameObject planet;
    public GameObject targetBee;
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var targetRotation = Quaternion.LookRotation(targetBee.transform.position - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
