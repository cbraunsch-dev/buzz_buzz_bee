using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public float timeToLive = 3.0f;

    private float timeSinceAlive = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAlive += Time.deltaTime;
        if(timeSinceAlive >= timeToLive)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    public void ReactToCollisionWithBee(Bee bee)
    {
        Debug.Log("Bee has hit flower");

        // Apply some force to the bee
        var upwardForceFactor = Random.Range(150.0f, 350.0f);
        var forwardForceFactor = Random.Range(-100.0f, 200.0f);
        var upwardForce = bee.gameObject.transform.up * upwardForceFactor;
        var forwardForce = bee.gameObject.transform.forward * forwardForceFactor;
        bee.gameObject.GetComponent<Rigidbody>().AddForce(upwardForce + forwardForce);
    }
}
