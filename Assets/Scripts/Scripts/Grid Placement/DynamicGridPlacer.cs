using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DynamicGridPlacer 
{ public static void PlaceObjects(List<Transform> objects, int aspectRatioX, int aspectRatioY, float spacingX, float spacingY)
    {
        var camera = Camera.main;
        int totalObjects = objects.Count;

        // Calculate the number of columns and rows based on the aspect ratio and total objects
        float ratio = (float)aspectRatioX / aspectRatioY;
        int rows = Mathf.CeilToInt(Mathf.Sqrt(totalObjects / ratio));  // Calculate number of rows
        int columns = Mathf.CeilToInt((float)totalObjects / rows);     // Calculate number of columns

        int fullRows = totalObjects / columns; // Number of full rows
        int remainder = totalObjects % columns; // Items left after full rows

        // Calculate total grid width and height
        float gridWidth = (columns - 1) * spacingX;
        float gridHeight = (fullRows - 1 + (remainder > 0 ? 1 : 0)) * spacingY;

        // Calculate the offset to center the grid in relation to the camera
        Vector3 cameraCenter = camera.transform.position + camera.transform.forward * 10f; // Set the Z distance to 10 units in front of the camera
        Vector3 gridCenterOffset = new Vector3(gridWidth / 2f, -gridHeight / 2f, 0); // Offset based on grid width/height

        int currentIndex = 0;

        // Place full rows
        for (int row = 0; row < fullRows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = new Vector3(col * spacingX, -row * spacingY, 0) - gridCenterOffset;
                objects[currentIndex].position = cameraCenter + position;
                currentIndex++;
            }
        }

        // Place remainder items in the center of the last row
        if (remainder > 0)
        {
            float startX = 0f;
            for (int col = 0; col < remainder; col++)
            {
                Vector3 position = new Vector3(startX + col * spacingX, -fullRows * spacingY, 0) - gridCenterOffset;
                objects[currentIndex].position = cameraCenter + position;
                currentIndex++;
            }
        }
    }
}
