using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappingBox : MonoBehaviour
{
    public float xMin = -63f;
    public float xMax = 63f;
    public float yMin = -28f;
    public float yMax = 28f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < xMin)
        {
            transform.position = new Vector3(xMax, transform.position.y, transform.position.z);
            //transform.position.Set(xMax, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xMax)
        {
            transform.position = new Vector3(xMin, transform.position.y, transform.position.z);
            //transform.position.Set(xMin, transform.position.y, transform.position.z);
        }

        if (transform.position.y < yMin)
        {
            transform.position = new Vector3(transform.position.x, yMax, transform.position.z);
            //transform.position.Set(transform.position.x, yMax, transform.position.z);
        }
        if (transform.position.y > yMax)
        {
            transform.position = new Vector3(transform.position.x, yMin, transform.position.z);
            //transform.position.Set(transform.position.x, yMin, transform.position.z);
        }
    }
}
