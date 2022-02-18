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
    IMaze maze;


    // Start is called before the first frame update
    void Start()
    {
        maze = new DFSMaze();
        //StartCoroutine( DebugMaze() );
        ReadMaze();
    }

    void InitTiles()
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
        InitTiles();
        maze.GenerateMaze(gridSize);
        for (int i = 0; i < gridSize * gridSize; i++)
        {
            Cell cell = maze.grid[i];
            tiles[cell.Pos].ReadCell(cell.Walls);
        }
    }

    IEnumerator DebugMaze()
    {
        InitTiles();
        tiles[maze.Init(gridSize)].SetVisited(true);

        while (true)
        {
            int[] indices = maze.Step(); //get neighborIndex and currentIndex from step
            if (indices.Length < 1) break;

            tiles[indices[0]].MarkLocated(true);

            if (indices.Length > 1)
            {
                foreach (int position in indices)
                {
                    tiles[position].ReadCell(maze.grid[position].Walls);
                }
            }

            yield return new WaitForSeconds(0.01f);
            tiles[indices[0]].MarkLocated(false);
        }
        Debug.Log("Maze generation finished");
    }

}
