using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    public GameObject player;

    public GameObject[] prefabs;

    public float spawnRate = 1f;
    public int amountAsteroidsPerSpawn = 1;

    /// <summary>
    /// Radius distance used when splitting asteroid
    /// </summary>
    public float distanceFromPreviousAsteroid = 14f;
    public float distanceFromPlayer = 14f;

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
        //Spawn 1 more asteroid every 2 minutes
        amountAsteroidsPerSpawn = 1 + (int)(Time.timeSinceLevelLoad / (2 * 60));
    }

    /// <summary>
    /// Instantiate 2 new asteroid close to a position (previous asteroid location)
    /// </summary>
    /// <param name="previousType"></param>
    /// <param name="position"></param>
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
    /// Spawn asteroids inside the wrapping box. Randomize prefab and asteroid size.
    /// </summary>
    public void Spawn()
    {
        if (numberOfAsteroid <= maximumNumberOfAsteroid - amountAsteroidsPerSpawn)
        {
            for (int i = 0; i < amountAsteroidsPerSpawn; i++)
            {
                Vector3 spawnPoint;
                do
                {
                    spawnPoint = WrappingBox.createSpawnPointInWrappingBox();
                } while (Vector3.Distance(spawnPoint, player.transform.position) < distanceFromPlayer); 
                GameObject a = Instantiate(prefabs[Random.Range(0/*int*/, prefabs.Length/*int*/)], spawnPoint, Quaternion.identity);
                a.GetComponent<Asteroid>().asteroidType = (EnumList.AsteroidsType)Random.Range(0/*int*/, prefabs.Length/*int*/);
            }
        }
    }
}
