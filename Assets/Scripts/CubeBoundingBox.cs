using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CubeBoundingBox : MonoBehaviour
{
    /// <summary>
    /// Initial vertices of the bounding box in localSpace
    /// </summary>
    private Vector3[] vertices; 
    
    /// <summary>
    /// Faces normals
    /// </summary>
    private Vector3[] axes;

    /// <summary>
    /// Vertices without duplicate in World Space
    /// </summary>
    Vector3[] verts; 

    public Transform Transform
    {
        get
        {
            return gameObject.transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetVerticesInWorld();
    }


    /// <summary>
    /// Return Bounding Box vertices without duplicate in world space
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetVerticesInWorld()
    {
        vertices = GetComponent<MeshFilter>().mesh.vertices;
        verts = vertices.Distinct().ToArray();
        for (int i = 0; i < verts.Length; i++) verts[i] = transform.TransformPoint(verts[i]);
        return verts;
    }

    /// <summary>
    /// Return the axes of the bounding box (ie. faces normals)
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetAxes()
    {
        axes = new[]
        {
            (this.transform.right),
            (this.transform.up),
            (this.transform.forward)
        };
        return axes;

    }
}
