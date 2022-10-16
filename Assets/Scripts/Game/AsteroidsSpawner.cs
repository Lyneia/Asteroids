using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{

    public GameObject[] prefabs;

    public float spawnRate = 1f;
    public int amountAsteroidsPerSpawn = 1;


    /// <summary>
    /// Radius distance used when splitting asteroid
    /// </summary>
    public float distanceFromPreviousAsteroid = 14f;

    /// <summary>
    /// Maximum number of asteroid spawned at the same time to prevent overcharged
    /// </summary>
    public int maximumNumberOfAsteroid = 500;
    public int numberOfAsteroid = 0;

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
        if (numberOfAsteroid <= maximumNumberOfAsteroid - 2)
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
                a.GetComponent<Asteroid>().asteroidType = EnumList.AsteroidsType.Medium;
                b.GetComponent<Asteroid>().asteroidType = EnumList.AsteroidsType.Medium;
            }
            if (previousType == EnumList.AsteroidsType.Medium)
            {
                a.GetComponent<Asteroid>().asteroidType = EnumList.AsteroidsType.Minor;
                b.GetComponent<Asteroid>().asteroidType = EnumList.AsteroidsType.Minor;
            }
        }
        
    }

    /// <summary>
    /// Spawn asteroid inside the wrapping box. Randomize prefab and asteroid size.
    /// </summary>
    public void Spawn()
    {
        if (numberOfAsteroid <= maximumNumberOfAsteroid - amountAsteroidsPerSpawn)
        {
            for (int i = 0; i < amountAsteroidsPerSpawn; i++)
            {
                Vector3 spawnPoint = WrappingBox.createSpawnPointInWrappingBox();
                GameObject a = Instantiate(prefabs[Random.Range(0/*int*/, prefabs.Length/*int*/)], spawnPoint, Quaternion.identity);
                a.GetComponent<Asteroid>().asteroidType = (EnumList.AsteroidsType)Random.Range(0/*int*/, prefabs.Length/*int*/);
            }
        }
    }
}
