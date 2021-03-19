using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    private GameObject planet;
    public GameObject angryBeePrefab;
    public bool AbleToGetPollinated { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.FindGameObjectWithTag(Tags.Ground);
        AbleToGetPollinated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReactToCollisionWithBee()
    {
        Debug.Log("Bee has hit flower - able to get pollinated: " + this.AbleToGetPollinated + " - tag: " + this.tag + " currently pollinating tag: " + planet.GetComponent<Planet>().FlowerColorCurrentlyBeingPollinated);
        if(AbleToGetPollinated && planet.GetComponent<Planet>().FlowerColorCurrentlyBeingPollinated == this.tag)
        {
            // Not so clean triggering these transitions from outside as these triggers then remain
            // active until they are "consumed". This requires knowledge of the state machine structure.
            // However I currently cannot think of a better way to solve this. Plus the state machine is linear
            // so this should never be an issue.
            GetComponent<Animator>().SetTrigger(Triggers.Pollinated);
        }
    }

    public void DidPollinateFlower()
    {
        planet.GetComponent<Planet>().FlowerWasPollinated(gameObject.tag);
    }

    public void SpawnAngryBee()
    {
        //var angryBee = Instantiate(angryBeePrefab);
        //angryBee.GetComponent<AngryBee>().targetBee = GameObject.FindGameObjectWithTag(Tags.Player);
    }
}
