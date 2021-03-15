using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeCamera : MonoBehaviour
{
    public GameObject bee;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(bee.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
