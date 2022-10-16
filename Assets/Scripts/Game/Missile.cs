using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float movementSpeed = 0.05f;

    public float maxLifeTime = 2f;
    private float lifeTime = 0f;

    private bool asCollideRecently = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector3.up;
        transform.position += dir * movementSpeed;
        lifeTime = lifeTime + Time.deltaTime;
        if (lifeTime > maxLifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Method called by CollisionController when one happen
    /// </summary>
    public void onCollide()
    {
        asCollideRecently = true;
        Destroy(this.gameObject);
    }

    public bool canCollide()
    {
        return !asCollideRecently;
    }
}
