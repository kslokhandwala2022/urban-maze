using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public CityBlock[] Walls;
    public GameObject Road;
    public GameObject Arrow;
    public Vector2 Pos;
    public int DirectionToFinish;
    public int DistanceFromFinish;


    //for grid
    public const float length = 5;

    //for debug
    public bool isLocated;
    private bool isVisited;
    Color ogColor;
    [SerializeField] Game game;
    [SerializeField] Color visitColor;
    [SerializeField] Color locateColor;

    public void SetVisited(bool visited)
    {
        isVisited = visited;
        Color before = Road.GetComponent<Renderer>().material.color;
        Road.GetComponent<Renderer>().material.color = new Color(before.r + .2f, before.g, before.b, before.a);
    }

    public void SetWalls(int[] walls)
    {
        for (int i = 0; i < Walls.Length; i++)
        {
            Walls[i].gameObject.SetActive( !(walls[i] > 0) );
            if(walls[i] > 1)
            {
                Debug.Log(i + " wall : " +  walls[i]);
                DirectionToFinish = i;
            }
        }
    }

    public void ReadCell(Cell cell)
    {
        SetWalls(cell.Walls);
        SetVisited(true);
        DistanceFromFinish = cell.DistanceFromOrigin;
        Arrow.SetActive(game.community > Random.Range(0, 100));
        //Debug
        if(game.debug)
        {
            Color before = Road.GetComponent<Renderer>().material.color;
            Road.GetComponent<Renderer>().material.color = new Color(before.r, DistanceFromFinish / 70f, before.b, before.a);
        }
        SetArrow(cell.SolutionDirection);
    }

    public void MarkLocated(bool visit)
    {
        Color before = Road.GetComponent<Renderer>().material.color;
        if (visit)
        {
            Road.GetComponent<Renderer>().material.color = new Color(before.r + .2f, before.g, 1.0f, before.a);
        }
        else
        {
            Road.GetComponent<Renderer>().material.color = new Color(before.r, before.g, ogColor.b, ogColor.a);
        }
    }

    public void SetArrow(int direction)
    {
        Vector3 res = Vector3.zero;
        switch(direction)
        {
            case 1:
                res.x = 0;
                break;
            case 2:
                res.y = 90;
                break;
            case 3:
                res.x = 180;
                break;
            case 0:
                res.x = 180;
                res.y = 90;
                break;
            default:
                Debug.Log("invalid direction");
                res.x = -90;
                break;

        }
        Arrow.transform.rotation = Quaternion.Euler(res);
        //arrow.transform.LookAt(res * 100);
    }

    private void Start()
    {
        ogColor = Road.GetComponent<Renderer>().material.color;
        //SetVisited(true);
    }


}
