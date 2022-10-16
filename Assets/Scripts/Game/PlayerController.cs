using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject explosionPlayerPrefab;

    public float movementSpeed = 1f;
    public float rotationSpeed = 10f;

    /// <summary>
    /// Prefab of a missile
    /// </summary>
    public GameObject missilePrefab;

    public Transform missileSpawn;


    /// <summary>
    /// Shot per second
    /// </summary>
    public float firerate;

    private float nextFireTimer;

    public int life = 3;

    private float immunityTime = 0;

    /// <summary>
    /// Immunity duration used by player to move without collinding instantly after respawn
    /// </summary>
    public float immunityDuration = 1.5f;

    public GameController gameController;

    /// <summary>
    /// Bool used to prevent object to collide 2 time in CollisionController
    /// </summary>
    public bool asCollideRecently = false;

    // Start is called before the first frame update
    void Start()
    {
        nextFireTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();

        }
        if (immunityTime < immunityDuration)
        {
            immunityTime = immunityTime + Time.deltaTime;
        }
    }

    public void Fire()
    {
        if (missilePrefab != null)
        {
            if (Time.time > nextFireTimer)
            {
                nextFireTimer = Time.time + firerate;
                GameObject missile = Instantiate(missilePrefab, missileSpawn.position, transform.rotation);
            }
        }
    }

    public void FixedUpdate()
    {
        float thrustValue = Input.GetAxis ("Vertical");
        float rotateValue = Input.GetAxis("Horizontal");

        Vector3 dir = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector3.up;
        transform.position += dir * (thrustValue>=0? thrustValue:0) * movementSpeed;

        if (rotateValue != 0)
        {
            transform.Rotate(0f,0f,-rotateValue*rotationSpeed);
        }
    }

    /// <summary>
    /// Method called by CollisionController when one happen
    /// </summary>
    public void onCollide()
    {
        Instantiate(explosionPlayerPrefab, transform.position, transform.rotation);

        life--;
        asCollideRecently = true;
        ///Gameover
        if (life <= 0)
        {
            gameController.gameOver();
            Destroy(this.gameObject);
        }
        else
        {
            gameController.respawnPlayer();
        }

        
        
    }

    public bool isImmune()
    {
        return immunityTime < immunityDuration;
    }

    public bool canCollide()
    {
        return !asCollideRecently && !isImmune();
    }
}
