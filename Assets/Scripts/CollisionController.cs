using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{

    private GameObject[] asteroids;
    private GameObject player;
    private GameObject[] missiles;


    /// <summary>
    /// List of damageable gameobject such as player, asteroids and missiles.
    /// </summary>
    private List<GameObject> damageableObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        asteroids = GameObject.FindGameObjectsWithTag("Asteroids");
        missiles = GameObject.FindGameObjectsWithTag("Missiles");
        player = GameObject.FindGameObjectWithTag("Player");

        damageableObjects = new List<GameObject>();
        if (asteroids != null && asteroids.Length >= 1)
        {
            damageableObjects.AddRange(asteroids);
        }
        if (missiles != null && missiles.Length >= 1)
        {
            damageableObjects.AddRange(missiles);
        }
        if (player != null)
        {
            damageableObjects.Add(player);
        }

        //For each damageable gameObject
        for (int i = 0; i < damageableObjects.Count; i++)
        {
            //Check the collision with others
            for (int j = 0; j < damageableObjects.Count; j++)
            {
                //If the gameobject isn't himself
                if (i != j && damageableObjects[i] != null && damageableObjects[j] != null)
                {
                    if (SeparatingAxisTheorem.CheckCollision(damageableObjects[i].GetComponent<CubeBoundingBox>(), damageableObjects[j].GetComponent<CubeBoundingBox>()))
                    {
                        //Object 1 collision
                        if (damageableObjects[i].tag == "Asteroids")
                        {
                            if (damageableObjects[j].tag == "Asteroids" && damageableObjects[i].GetComponent<Asteroid>().canCollideWithAsteroid())
                            {
                                damageableObjects[i].GetComponent<Asteroid>().onCollide();
                            }
                            if ((damageableObjects[j].tag == "Player" || damageableObjects[j].tag == "Missiles") && damageableObjects[i].GetComponent<Asteroid>().canCollide())
                            {
                                damageableObjects[i].GetComponent<Asteroid>().onCollide();
                            }
                        }
                        if (damageableObjects[i].tag == "Player")
                        {
                            if (damageableObjects[j].tag == "Asteroids")
                            {
                                if (damageableObjects[i].GetComponent<PlayerController>().canCollide())
                                {
                                    damageableObjects[i].GetComponent<PlayerController>().onCollide();
                                }
                            }
                        }
                        if (damageableObjects[i].tag == "Missiles")
                        {
                            if (damageableObjects[j].tag == "Asteroids")
                            {
                                if (damageableObjects[i].GetComponent<Missile>().canCollide())
                                {
                                    damageableObjects[i].GetComponent<Missile>().onCollide();
                                }
                            }
                        }
                        //

                        //Object 2 collsion
                        if (damageableObjects[j].tag == "Asteroids")
                        {
                            if (damageableObjects[i].tag == "Asteroids" && damageableObjects[j].GetComponent<Asteroid>().canCollideWithAsteroid())
                            {
                                damageableObjects[j].GetComponent<Asteroid>().onCollide();
                                
                            }
                            if ((damageableObjects[i].tag == "Player" || damageableObjects[i].tag == "Missiles") && damageableObjects[j].GetComponent<Asteroid>().canCollide())
                            {
                                damageableObjects[j].GetComponent<Asteroid>().onCollide();
                            }
                        }
                        if (damageableObjects[j].tag == "Player")
                        {
                            if (damageableObjects[i].tag == "Asteroids")
                            {
                                if (damageableObjects[j].GetComponent<PlayerController>().canCollide())
                                {
                                    damageableObjects[j].GetComponent<PlayerController>().onCollide();
                                }
                            }
                        }
                        if (damageableObjects[j].tag == "Missiles")
                        {
                            if (damageableObjects[i].tag == "Asteroids")
                            {
                                if (damageableObjects[j].GetComponent<Missile>().canCollide())
                                {
                                    damageableObjects[j].GetComponent<Missile>().onCollide();
                                }
                            }
                        }
                        //
                    }

                }
            }
        }
    }
}
