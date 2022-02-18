using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public CityBlock[] Walls;
    public GameObject road;
    public Vector2 pos;
    public int directionToFinish;



    //for grid
    public const float length = 5;

    //for debug
    public bool isLocated;
    private bool isVisited;
    Color ogColor;
    [SerializeField] Color visitColor;
    [SerializeField] Color locateColor;

    public void SetVisited(bool visited)
    {
        isVisited = visited;
        Color before = road.GetComponent<Renderer>().material.color;
        road.GetComponent<Renderer>().material.color = new Color(before.r + .2f, before.g - .2f, before.b, before.a);
    }

    public void SetWalls(int[] walls)
    {
        for (int i = 0; i < Walls.Length; i++)
        {
            Walls[i].gameObject.SetActive( !(walls[i] > 0) );
            if(walls[i] > 1)
            {
                Debug.Log(i + " wall : " +  walls[i]);
                directionToFinish = i;
            }
        }
    }

    public  void ReadCell(int[] walls)
    {
        SetWalls(walls);
        SetVisited(true);
    }

    public void MarkLocated(bool visit)
    {
        Color before = road.GetComponent<Renderer>().material.color;
        if (visit)
        {
            road.GetComponent<Renderer>().material.color = new Color(before.r + .2f, before.g - .2f, 1.0f, before.a);
        }
        else
        {
            road.GetComponent<Renderer>().material.color = new Color(before.r, before.g, ogColor.b, ogColor.a);
        }
    }

    private void Start()
    {
        ogColor = road.GetComponent<Renderer>().material.color;
        //SetVisited(true);
    }


}
