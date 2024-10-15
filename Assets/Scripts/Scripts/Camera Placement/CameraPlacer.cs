using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraPlacer 
{
    
    public static void PlaceCameraToFitPoints( Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4)
    {
        var camera = Camera.main;
        Vector3[] points = new Vector3[] { point1, point2, point3, point4 };
    
        // Calculate the center of the points
        Vector3 centerPoint = CalculateCenterPoint(points);

        // Move the camera to the center point
        camera.transform.position = new Vector3(centerPoint.x, centerPoint.y, camera.transform.position.z);

        // Calculate the required orthographic size
        float requiredOrthoSize = CalculateOrthographicSize(points, centerPoint);

        // Set the camera's orthographic size
        camera.orthographicSize = requiredOrthoSize;
    }

    /// <summary>
    /// Calculate the center point between four points.
    /// </summary>
    private static Vector3 CalculateCenterPoint(Vector3[] points)
    {
        Vector3 sum = Vector3.zero;
        foreach (Vector3 point in points)
        {
            sum += point;
        }
        return sum / points.Length;
    }
    private static float CalculateOrthographicSize( Vector3[] points, Vector3 centerPoint)
    {
        float maxDistance = 0f;
    
        // Find the farthest distance between the center and the four points
        foreach (Vector3 point in points)
        {
            float distance = Vector3.Distance(point, centerPoint);
            if (distance > maxDistance)
            {
                maxDistance = distance;
            }
        }
        float aspectRatio = (float)Screen.width / Screen.height;
       
        float orthographicSize = maxDistance / aspectRatio;
       
        return orthographicSize;
    }
}
