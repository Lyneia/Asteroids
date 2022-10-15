using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public EnumList.AsteroidsType asteroidType = EnumList.AsteroidsType.Major;

    public AsteroidsSpawner asteroidSpawner;

    /// <summary>
    /// Movement direction for the asteroid
    /// </summary>
    Vector3 direction;

    public float movementSpeed = 0.02f;
    public float minMovementSpeed = 0.01f;
    public float maxMovementSpeed = 0.06f;
    public float rotationSpeed = 1.2f;
    public float minRotationSpeed = 0.8f;
    public float maxRotationSpeed = 2f;

    public float immunityTime = 0;

    /// <summary>
    /// Immunity duration used to move asteroids without collinding instantly after spawn
    /// </summary>
    public float immunityDuration = 5f;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        direction = Random.insideUnitCircle;
        asteroidSpawner = GameObject.FindGameObjectWithTag("GameController").GetComponent<AsteroidsSpawner>();

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
        if (asteroidType != EnumList.AsteroidsType.Minor)
        {
            asteroidSpawner.splitAsteroid(asteroidType, transform.position);
        }

        Destroy(this.gameObject);
        
    }
}
