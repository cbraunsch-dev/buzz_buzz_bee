using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject bee;
    public GameObject flower1Prefab;

    private List<Flower> flowers = new List<Flower>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 15; i++)
        {
            flowers.Add(SpawnFlower());
        }
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

    public List<Flower> Flowers
    {
        get
        {
            return this.flowers;
        }
    }
}
