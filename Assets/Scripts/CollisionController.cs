using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{

    public GameObject[] asteroids;
    public GameObject player;
    public GameObject[] missiles;


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
                if (i != j)
                {
                    if (SeparatingAxisTheorem.CheckCollision(damageableObjects[i].GetComponent<CubeBoundingBox>(), damageableObjects[j].GetComponent<CubeBoundingBox>()))
                    {
                        if (damageableObjects[i].tag == "Asteroids")
                        {
                            if (damageableObjects[j].tag == "Asteroids")
                            {
                                if (damageableObjects[i].GetComponent<Asteroids>().immunityTime > damageableObjects[i].GetComponent<Asteroids>().immunityDuration)
                                {
                                    damageableObjects[i].GetComponent<Asteroids>().onCollide();
                                }
                            }
                            if (damageableObjects[j].tag == "Asteroids")
                            {
                                if (damageableObjects[i].tag == "Asteroids")
                                {
                                    if (damageableObjects[j].GetComponent<Asteroids>().immunityTime > damageableObjects[j].GetComponent<Asteroids>().immunityDuration)
                                    {
                                        damageableObjects[j].GetComponent<Asteroids>().onCollide();
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}
