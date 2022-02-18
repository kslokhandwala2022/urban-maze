using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reads in grid array and draws maze out using prefabs
public class MazeGenerator : MonoBehaviour
{

    private int scale;
    private const int gridSize = 10; //this will come from maze object soon
    [SerializeField] private Tile tile;
    [SerializeField] private List<Tile> tiles;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine( DebugMaze() );
        ReadMaze();
    }

    void InitMaze()
    {
        //for now draw grid
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                tiles.Add( Instantiate(tile, new Vector3(i * Tile.length, 0, j * Tile.length), Quaternion.identity) );
            }
        }
    }

    void ReadMaze()
    {
        InitMaze();
        DFSMaze.generateMaze(gridSize);
        for (int i = 0; i < gridSize * gridSize; i++)
        {
            DFSMaze.Cell cell = DFSMaze.grid[i];
            Tile currentTile = tiles[cell.Pos];
            currentTile.SetWalls(cell.Walls);
            currentTile.SetVisited(true);

        }
    }

    IEnumerator DebugMaze()
    {
        InitMaze();
        DFSMaze.size = gridSize;
        DFSMaze.init(gridSize);

        //initial cell, for now, 0, 0
        DFSMaze.Cell initial = DFSMaze.grid[0];

        initial.isVisited = true;
        tiles[initial.Pos].SetVisited(true);
        DFSMaze.stack.Push(initial);

        DFSMaze.Cell currentCell;
        int neighbor;
        while (DFSMaze.stack.Count > 0)
        {
            currentCell = DFSMaze.stack.Pop();
            neighbor = currentCell.getNeighbor();

            //to see currlocation
            Tile currentTile = tiles[currentCell.Pos];
            currentTile.MarkLocated(true);

            if (neighbor > 0)//has a valid neighbor
            {
                //Debug.Log("At " + currentCell.Pos + " going to :" + neighbor);
                DFSMaze.stack.Push(currentCell);
                DFSMaze.removeWall(currentCell, DFSMaze.grid[neighbor]);
                tiles[neighbor].SetWalls(currentCell.Walls);

                DFSMaze.Cell neighborCell = DFSMaze.grid[neighbor];
                neighborCell.isVisited = true;
                DFSMaze.stack.Push(neighborCell);

                //match tiles to changes
                currentTile.SetWalls(currentCell.Walls);
                tiles[neighbor].SetWalls(neighborCell.Walls);
                tiles[neighbor].SetVisited(true);

            }
            yield return new WaitForSeconds(0.1f);
            currentTile.MarkLocated(false);
        }

        Debug.Log("Maze generation finished");
        //ReadMaze();
        

    }

}
