using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject bee;
    public GameObject flower1Prefab;

    private List<GameObject> allFlowers = new List<GameObject>();
    private List<GameObject> redFlowers = new List<GameObject>();
    private List<GameObject> greenFlowers = new List<GameObject>();
    private List<GameObject> blueFlowers = new List<GameObject>();
    private List<GameObject> yellowFlowers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        redFlowers = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.RedFlower));
        redFlowers.ForEach(flower =>
        {
            allFlowers.Add(flower);
        });
        greenFlowers = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.GreenFlower));
        greenFlowers.ForEach(flower =>
        {
            allFlowers.Add(flower);
        });
        blueFlowers = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.BlueFlower));
        blueFlowers.ForEach(flower =>
        {
            allFlowers.Add(flower);
        });
        yellowFlowers = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.YellowFlower));
        yellowFlowers.ForEach(flower =>
        {
            allFlowers.Add(flower);
        });

        // Make red flowers bloom first
        redFlowers.ForEach(flower =>
        {
            // It is kinda dangerous to trigger the state machine from the outside but I currently can't think of a better way to do this
            flower.GetComponent<Animator>().SetTrigger(Triggers.StartGrowing);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Flower SpawnFlower()
    {
        var planetRadius = GetComponent<SphereCollider>().radius * transform.localScale.x;
        var flowerPos = Random.onUnitSphere * planetRadius;
        var flower = Instantiate(flower1Prefab);
        var planetPos = transform.position;
        flower.transform.position = flowerPos;
        flower.transform.up = (flowerPos - planetPos).normalized;
        return flower.GetComponent<Flower>();
    }

    public float Radius
    {
        get
        {
            return GetComponent<SphereCollider>().radius * transform.localScale.x;
        }
    }

    public List<GameObject> Flowers
    {
        get
        {
            return this.allFlowers;
        }
    }
}
