using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject bee;
    public GameObject flower1Prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn new flower
        var planetRadius = GetComponent<SphereCollider>().radius * transform.localScale.x;
        var beePos = bee.transform.position;
        var planetPos = transform.position;
        var flowerPos = (beePos - planetPos).normalized * planetRadius + planetPos;
        var flower = Instantiate(flower1Prefab);
        flower.transform.position = flowerPos;
        flower.transform.up = (flowerPos - planetPos).normalized;
    }
}
