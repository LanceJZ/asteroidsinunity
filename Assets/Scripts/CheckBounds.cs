using System.Collections;
using UnityEngine;

public class CheckBounds
{
    // Use this for class variables.
    public Vector2 gameBounds;

    public CheckBounds(Camera camera, Transform distanceFromCamera)
    {
        float distance = distanceFromCamera.position.z * -1;
        float frustumHeight = 2 * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = (frustumHeight * camera.aspect);

        gameBounds.x = frustumWidth * 0.5f;
        gameBounds.y = frustumHeight * 0.5f;
    }

    public Vector3 CheckOffScreen(Vector3 currentLocation)
    {
        Vector3 newLocation = currentLocation;

        if (currentLocation.x > gameBounds.x)
            newLocation = new Vector3(-gameBounds.x, currentLocation.y, currentLocation.z);

        if (currentLocation.x < -gameBounds.x)
            newLocation = new Vector3(gameBounds.x, currentLocation.y, currentLocation.z);

        if (currentLocation.y > gameBounds.y)
            newLocation = new Vector3(currentLocation.x, -gameBounds.y, currentLocation.z);

        if (currentLocation.y < -gameBounds.y)
            newLocation = new Vector3(currentLocation.x, gameBounds.y, currentLocation.z);

        return newLocation;
    }
}