using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    //this is the prefab
    //building settings
    public static float scale = 5;
    public static float height = 1.5f;
    public static int levels = 6;
    public static bool hasWindow = false;
    public static float windowBorder = .1f;
    public GameObject prefabBlock;
    public GameObject prefabWindow;

    #region Build function and overloads
    /**
     * Build and instantiate given location
     */
    public void Build(Vector3 position, float maxScale = 0)
    {
        transform.position = position;
        Transform buildingBlock = transform;
        //initial block
        ScaleRandomly(buildingBlock, Mathf.Min(scale, maxScale));

        for (int i = 0; i < levels; i++)
        {
            buildingBlock = Instantiate(prefabBlock, buildingBlock).transform;
            ScaleRandomly(buildingBlock);
            //scale the block first
            PlaceBlock(buildingBlock);
            //based on values, place the block
        }
    }

    /**
    * Build and instantiate given location
    */
    public void Build(Vector3 position, Vector2 maxScale)
    {
        transform.position = position;
        Transform buildingBlock = transform;
        ScaleRandomly(buildingBlock, Vector2.Min(maxScale, Vector2.one * scale));

        for (int i = 0; i < levels; i++)
        {
            buildingBlock = Instantiate(prefabBlock, buildingBlock).transform;
            ScaleRandomly(buildingBlock);
            PlaceBlock(buildingBlock);
        }
    }

    #endregion

    #region Helper functions
    void ScaleRandomly(Transform res, float scaleFactor = 1)
    {
        float x = Random.value / 2 + .4f;
        float y = Random.value / 2 + .4f;
        float z = Random.value / 2 + .4f;
        res.localScale = Random.Range(0.5f, 1.0f) * new Vector3(x * scaleFactor, y * height, z * scaleFactor);
    }

    void ScaleRandomly(Transform res, Vector2 scaleFactor)
    {
        float x = Random.value / 2 + .4f;
        float y = Random.value / 2 + .4f;
        float z = Random.value / 2 + .4f;
        res.localScale = Random.Range(0.5f, 1.0f) * new Vector3(x * scaleFactor.x, y * height, z * scaleFactor.y);
    }

    void PlaceBlock(Transform res)
    {
        Transform parent = res.parent;
        float x = parent.position.x + (Random.Range(-1, 2) * (parent.lossyScale.x - res.lossyScale.x) / 2);
        float z = parent.position.z + (Random.Range(-1, 2) * (parent.lossyScale.z - res.lossyScale.z) / 2);
        float y = parent.position.y + parent.lossyScale.y / 2 + res.lossyScale.y / 2;
        res.position = new Vector3(x, y, z);
    }

    #endregion

    #region window Placement
    void PlaceWindows(Transform res)
    {
        //create a grid of windows based on
        //x/y scale, z/y scale



    }

    #endregion


}
