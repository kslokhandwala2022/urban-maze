using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingMaskManager : MonoBehaviour
{
    public Camera camera;
    
    public bool playerVisible = false;
    public bool visitedRoadTilesVisible = false;
    public bool fixedObjectsVisible = false;
    public bool unfixedObjectsVisible = false;
    public bool coinsVisible = false;
    public bool goalVisible = false;
    public bool entireMapVisible = false;

    public void UpdateMapVisibility(int objectsFixed)
    {
        if(objectsFixed >= 3)
        {
            playerVisible = true;
            visitedRoadTilesVisible = true;
        }

        if (objectsFixed >= 5)
        {
            fixedObjectsVisible = true;
        }

        if (objectsFixed >= 7)
        {
            unfixedObjectsVisible = true;
        }

        if (objectsFixed >= 10)
        {
            coinsVisible = true;
        }

        if (objectsFixed >= 15)
        {
            goalVisible = true;
        }

        if (objectsFixed >= 20)
        {
            entireMapVisible = true;
        }

        UpdateCullingMask();
    }

    public void UpdateCullingMask()
    {

    }
}
