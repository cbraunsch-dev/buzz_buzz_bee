using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public GameObject angryBeePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReactToCollisionWithBee(Bee bee)
    {
        Debug.Log("Bee has hit flower");
        // TODO: tell state machine that flower has been pollinated
    }
}
