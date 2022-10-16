using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparatingAxisTheorem : MonoBehaviour
{
    // References
    // SAT explanation
    // https://dyn4j.org/2010/01/sat/
    //
    // Usefull reading
    // https://www.geometrictools.com/Documentation/DynamicCollisionDetection.pdf
    // https://gamedev.stackexchange.com/questions/44500/how-many-and-which-axes-to-use-for-3d-obb-collision-with-sat/
    //
    // Unity Implementation of SAT
    // https://github.com/irixapps/Unity-Separating-Axis-SAT

    /// <summary>
    /// Check if there is a collision between 2 cube bounding box
    /// </summary>
    /// <param name="boundingBoxA"></param>
    /// <param name="boundingBoxB"></param>
    /// <returns></returns>
    public static bool CheckCollision(CubeBoundingBox boundingBoxA, CubeBoundingBox boundingBoxB)
    {
        Vector3[] axesA = boundingBoxA.GetAxes();
        Vector3[] axesB = boundingBoxB.GetAxes();


        //All axes can be added dynamically, such as AllAxes = AxesFromA + AxesFromB + CrossProductOfAllAxesFromAAndB
        //Since we are using cube it's manually done here
        Vector3[] allAxes = new Vector3[]
        {
            axesA[0],
            axesA[1],
            axesA[2],
            axesB[0],
            axesB[1],
            axesB[2],
            Vector3.Cross(axesA[0], axesB[0]),
            Vector3.Cross(axesA[0], axesB[1]),
            Vector3.Cross(axesA[0], axesB[2]),
            Vector3.Cross(axesA[1], axesB[0]),
            Vector3.Cross(axesA[1], axesB[1]),
            Vector3.Cross(axesA[1], axesB[2]),
            Vector3.Cross(axesA[2], axesB[0]),
            Vector3.Cross(axesA[2], axesB[1]),
            Vector3.Cross(axesA[2], axesB[2])
        };

        Vector3[] verticesA = boundingBoxA.GetVerticesInWorld();
        Vector3[] verticesB = boundingBoxB.GetVerticesInWorld();

        return false || computeSAT(boundingBoxA.transform, boundingBoxB.transform, allAxes, verticesA, verticesB);
    }

    /// <summary>
    /// Separating Axis Theorem calculation. For a list of given axis, check if there is at least 1 axis for which the projection of the vertices don't overlap which me the corresponding convex objects aren't collinding.
    /// </summary>
    /// <param name="transformA"></param>
    /// <param name="transformB"></param>
    /// <param name="allAxes"></param>
    /// <param name="verticesA"></param>
    /// <param name="verticesB"></param>
    /// <returns></returns>
    private static bool computeSAT(Transform transformA, Transform transformB, Vector3[] allAxes, Vector3[] verticesA, Vector3[] verticesB)
    {
        float minOverlap = float.PositiveInfinity;

        for (int i = 0; i < allAxes.Length; i++)
        {
            float projMinB = float.MaxValue, projMinA = float.MaxValue;
            float projMaxB = float.MinValue, projMaxA = float.MinValue;

            Vector3 axis = allAxes[i];

            // Handles the cross product = {0,0,0} case
            if (allAxes[i] == Vector3.zero) return true;

            for (int j = 0; j < verticesA.Length; j++)
            {
                float val = CalcScalarProjection((verticesA[j]), axis);

                if (val < projMinA)
                {
                    projMinA = val;
                }

                if (val > projMaxA)
                {
                    projMaxA = val;
                }
            }

            for (int j = 0; j < verticesB.Length; j++)
            {
                float val = CalcScalarProjection((verticesB[j]), axis);

                if (val < projMinB)
                {
                    projMinB = val;
                }

                if (val > projMaxB)
                {
                    projMaxB = val;
                }
            }

            float overlap = FindOverlap(projMinA, projMaxA, projMinB, projMaxB);

            if (overlap < minOverlap)
            {
                //Debug.Log("Overlap : " + overlap + " minOverlap : " + minOverlap+" minOverlapAxis : "+axis);
                minOverlap = overlap;
            }

            if (overlap <= 0)
            {
                // Separating Axis Early Out
                return false;
            }
        }

        return true; // A penetration has been found
    }


    /// <summary>
    /// Calculate Scalar Projection of a point in an axis assuming normalised values
    /// </summary>
    /// <param name="p"></param>
    /// <param name="axis"></param>
    /// <returns></returns>
    private static float CalcScalarProjection(Vector3 p, Vector3 axis)
    {
        return Vector3.Dot(p, axis);
    }

    /// <summary>
    /// Calculates the amount of overlap of two intervals.
    /// </summary>
    /// <param name="startA"></param>
    /// <param name="endA"></param>
    /// <param name="startB"></param>
    /// <param name="endB"></param>
    /// <returns></returns>
    private static float FindOverlap(float startA, float endA, float startB, float endB)
    {
        if (startA < startB)
        {
            if (endA < startB)
            {
                return 0f;
            }

            return endA - startB;
        }

        if (endB < startA)
        {
            return 0f;
        }

        return endB - startA;
    }
}
