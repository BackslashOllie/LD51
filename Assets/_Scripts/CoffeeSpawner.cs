using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeSpawner : MonoBehaviour
{
    public GameObject coffeeCup;
    public Transform spawnLocation;

    public GameEvent gEvent;

    public float minSpawnDelay, maxSpawnDelay;

    public int strayFactor = 10;

    float spawnTime, delay;

    private void Start()
    {
        spawnTime = Time.time;
        delay = Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    private void Update()
    {
        if(!gEvent.isActive && Time.time > spawnTime + delay)
        {
            CreateCoffee();
        }
    }

    public void CreateCoffee()
    {
        GameObject coffee = Instantiate(coffeeCup, spawnLocation.position, spawnLocation.rotation);
        var randomNumberX = Random.Range(-strayFactor, strayFactor);
        var randomNumberY = Random.Range(-strayFactor, strayFactor);
        var randomNumberZ = Random.Range(-strayFactor, strayFactor); ;
        coffee.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
        coffee.GetComponent<Rigidbody>().AddForce(spawnLocation.forward * (Random.Range(50, 250)));

        spawnTime = Time.time;
        delay = Random.Range(minSpawnDelay, maxSpawnDelay);
    }

}
