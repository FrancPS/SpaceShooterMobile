using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ----- Utils -----
 */
public static class Utils
{
    public static Vector2 GetScreenDimensionsInWorldUnits()
    {
        float width, height;

        Camera camera = Camera.main;
        height = camera.orthographicSize * 2;

        float aspectRatio = camera.pixelWidth / (float)camera.pixelHeight;
        width = height * aspectRatio;


        return new Vector2(width, height);
    }
}
