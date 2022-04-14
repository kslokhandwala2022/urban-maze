using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingMaskManager : MonoBehaviour
{
    public Camera camera;
    public Game settings;
    private int lastNumObjectsFixedVal = 0;
    
    // Mostly just placeholders, these track if something should be visible but dont actually make it visible
    public bool playerVisible = false; // Layer 8
    public bool visitedRoadTilesVisible = false; // Layer 14
    public bool fixedObjectsVisible = false; // Layer 15
    public bool unfixedObjectsVisible = false; // Layer 16
    public bool coinsVisible = false; // Layer 9
    public bool goalVisible = false; // Layer 17
    public bool entireMapVisible = false; // Layer 13

    // Hard-coded values to get the specific culling mask for each combination of layers
    private int cullingmask1 = (16640);
    private int cullingmask2 = (49408);
    private int cullingmask3 = (114944);
    private int cullingmask4 = (115456);
    private int cullingmask5 = (246528);
    private int cullingmask6 = (254720);

    // Writes to this value and feeds it to UpdateCullingMask()
    private int currentCullingMask = (0);
    
    
    // Checks # of fixed objects and adjusts the culling mask
    // on the minimap camera accordingly to make desired elements visible
    public void UpdateMapVisibility(int objectsFixed)
    {

        if(objectsFixed >= 3)
        {
            playerVisible = true;
            visitedRoadTilesVisible = true;
            currentCullingMask = cullingmask1;
        }

        if (objectsFixed >= 5)
        {
            fixedObjectsVisible = true;
            currentCullingMask = cullingmask2;
        }

        if (objectsFixed >= 7)
        {
            unfixedObjectsVisible = true;
            currentCullingMask = cullingmask3;
        }

        if (objectsFixed >= 10)
        {
            coinsVisible = true;
            currentCullingMask = cullingmask4;
        }

        if (objectsFixed >= 15)
        {
            goalVisible = true;
            currentCullingMask = cullingmask5;
        }

        if (objectsFixed >= 20)
        {
            entireMapVisible = true;
            currentCullingMask = cullingmask6;
        }

        UpdateCullingMask(currentCullingMask);
    }

    // This is called to actually change the culling mask
    public void UpdateCullingMask(int newCullingMask)
    {
        camera.cullingMask |= newCullingMask;
    }

    // Checks constantly to see how many objects have been fixed.
    // If its a new value, checks to see if it should update visibility.
    // Far from optimal.
    void Update()
    {
        if (settings.numObjectsFixed > lastNumObjectsFixedVal)
        {
            UpdateMapVisibility(settings.numObjectsFixed);
        }
    }
}
