using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public EnumList.AsteroidsType asteroidType = EnumList.AsteroidsType.Major;

    public AsteroidsSpawner asteroidSpawner;

    /// <summary>
    /// Movement direction for the asteroid
    /// </summary>
    Vector3 direction;

    public float movementSpeed = 0.02f;
    public float minMovementSpeed = 5f;
    public float maxMovementSpeed = 15f;
    public float rotationSpeed = 1.2f;
    public float minRotationSpeed = 50f;
    public float maxRotationSpeed = 150f;

    private float immunityTime = 0;

    /// <summary>
    /// Immunity duration used to move asteroids without collinding instantly after spawn
    /// </summary>
    private float immunityDuration = 5f;

    /// <summary>
    /// Bool used to prevent object to collide 2 time in CollisionController
    /// </summary>
    private bool asCollideRecently = false;

    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        direction = Random.insideUnitCircle;
        asteroidSpawner = GameObject.FindGameObjectWithTag("GameController").GetComponent<AsteroidsSpawner>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        switch (asteroidType)
        {
            case EnumList.AsteroidsType.Major:
                transform.localScale = new Vector3(10, 10, 10);
                break;
            case EnumList.AsteroidsType.Medium:
                transform.localScale = new Vector3(7, 7, 7);
                break;
            case EnumList.AsteroidsType.Minor:
                transform.localScale = new Vector3(5, 5, 5);
                break;
            default:
                transform.localScale = new Vector3(5, 5, 5);
                break;
        }


    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * Time.deltaTime * movementSpeed;
        transform.Rotate(direction * (rotationSpeed * Time.deltaTime));

        if (immunityTime < immunityDuration)
        {
            immunityTime = immunityTime + Time.deltaTime;
        }
    }

    /// <summary>
    /// Method called by CollisionController when one happen
    /// </summary>
    public void onCollide()
    {
        asCollideRecently = true;
        if (asteroidType != EnumList.AsteroidsType.Minor)
        {
            asteroidSpawner.splitAsteroid(asteroidType, transform.position);
        }
        asteroidSpawner.numberOfAsteroid = asteroidSpawner.numberOfAsteroid - 1;
        gameController.increaseScore(asteroidType);
        Destroy(this.gameObject);
        
    }

    public bool isImmuneToAsteroid()
    {
        return immunityTime < immunityDuration;
    }

    public bool canCollide()
    {
        return !asCollideRecently;
    }

    public bool canCollideWithAsteroid()
    {
        return !asCollideRecently && !isImmuneToAsteroid();
    }
}
