using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{

    public GameObject[] prefabs;

    public float spawnRate = 1f;
    public int amountPerSpawn = 1;

    public float xMin = -63f;
    public float xMax = 63f;
    public float yMin = -28f;
    public float yMax = 28f;

    /// <summary>
    /// Radius distance used when splitting asteroid
    /// </summary>
    public float distanceFromPreviousAsteroid = 14f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void splitAsteroid(EnumList.AsteroidsType previousType, Vector3 position)
    {
        Vector3 vectInCircle = Random.insideUnitCircle * distanceFromPreviousAsteroid;
        // Position for first asteroid spawn
        Vector3 pos = position + vectInCircle;
        // Second asteroid position
        Vector3 pos2 = position - vectInCircle;

        GameObject a = Instantiate(prefabs[Random.Range(0/*int*/, prefabs.Length/*int*/)], pos, Quaternion.identity);
        GameObject b = Instantiate(prefabs[Random.Range(0/*int*/, prefabs.Length/*int*/)], pos2, Quaternion.identity);
        if (previousType == EnumList.AsteroidsType.Major)
        {
            a.GetComponent<Asteroids>().asteroidType = EnumList.AsteroidsType.Medium;
            b.GetComponent<Asteroids>().asteroidType = EnumList.AsteroidsType.Medium;
        }
        if (previousType == EnumList.AsteroidsType.Medium)
        {
            a.GetComponent<Asteroids>().asteroidType = EnumList.AsteroidsType.Minor;
            b.GetComponent<Asteroids>().asteroidType = EnumList.AsteroidsType.Minor;
        }
        
    }

    /// <summary>
    /// Spawn asteroid inside the wrapping box. Randomize prefab and asteroid size.
    /// </summary>
    public void Spawn()
    {
        for (int i = 0; i < amountPerSpawn; i++)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(xMin, xMin), Random.Range(yMin, yMin), 0);
            GameObject a = Instantiate(prefabs[Random.Range(0/*int*/, prefabs.Length/*int*/)], spawnPoint, Quaternion.identity);
            a.GetComponent<Asteroids>().asteroidType = (EnumList.AsteroidsType)Random.Range(0/*int*/, prefabs.Length/*int*/);
        }
    }
}
