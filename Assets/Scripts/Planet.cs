using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject bee;
    public GameObject flower1Prefab;
    public float timeBetweenFlowerSpawns = 2.0f;

    private float timeSinceLastFlowerSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastFlowerSpawn += Time.deltaTime;
        if(timeSinceLastFlowerSpawn >= timeBetweenFlowerSpawns)
        {
            timeSinceLastFlowerSpawn = 0.0f;
            SpawnFlower();
        }
    }

    private void SpawnFlower()
    {
        var planetRadius = GetComponent<SphereCollider>().radius * transform.localScale.x;
        var beePos = bee.transform.position;
        var rangeFactor = 10.0f;
        var variance = new Vector3(Random.Range(-rangeFactor, rangeFactor), 0, Random.Range(-rangeFactor, rangeFactor));
        var beePosWithVariance = beePos + variance;
        var planetPos = transform.position;
        var flowerPos = (beePosWithVariance - planetPos).normalized * planetRadius + planetPos;
        var flower = Instantiate(flower1Prefab);
        flower.transform.position = flowerPos;
        flower.transform.up = (flowerPos - planetPos).normalized;
    }

    public float Radius
    {
        get
        {
            return GetComponent<SphereCollider>().radius * transform.localScale.x;
        }
    }
}
