using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappingBox : MonoBehaviour
{
    public static float xMin = -55f;
    public static float xMax = 55f;
    public static float yMin = -30f;
    public static float yMax = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkPosition();
    }

    /// <summary>
    /// Check if gameobject is inside WrappingBox
    /// </summary>
    public void checkPosition()
    {
        if (transform.position.x < xMin)
        {
            transform.position = new Vector3(xMax, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xMax)
        {
            transform.position = new Vector3(xMin, transform.position.y, transform.position.z);
        }

        if (transform.position.y < yMin)
        {
            transform.position = new Vector3(transform.position.x, yMax, transform.position.z);
        }
        if (transform.position.y > yMax)
        {
            transform.position = new Vector3(transform.position.x, yMin, transform.position.z);
        }
    }

    public static Vector3 createSpawnPointInWrappingBox()
    {
        return new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
    }
}
