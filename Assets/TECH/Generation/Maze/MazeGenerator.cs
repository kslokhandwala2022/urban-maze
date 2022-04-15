using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reads in grid array and draws maze out using prefabs
public class MazeGenerator : MonoBehaviour
{

    [SerializeField] private Tile tile;
    [SerializeField] private List<Tile> tiles;
    [SerializeField] private GameObject player;
    [SerializeField] private MazeUIManager ui;

    IMaze maze;
    [SerializeField] private Game game;


    // Start is called before the first frame update
    void Start()
    {
        maze = new DFSMaze();
        if( game.debug )
        {
            StartCoroutine( DebugMaze() );
        }
        else
        {
            ReadMaze();
        }
        //place player at start square
        player.transform.position = tiles[maze.origin.Pos].transform.position;
        tiles[maze.destination.Pos].EndZone.SetActive(true);
        //init player speed
        ui.InitGUI();

    }

    private void Update()
    {
        ui.UpdateTimer();
        ui.UpdateWealth();
    }

    void InitTiles()
    {
        //for now draw grid
        for (int i = 0; i < game.mazeSize; i++)
        {
            for (int j = 0; j < game.mazeSize; j++)
            {
                tiles.Add( Instantiate(tile, new Vector3(i * Tile.length, 0, j * Tile.length), Quaternion.identity) );
            }
        }
    }

    void ReadMaze()
    {
        InitTiles();
        maze.GenerateMaze(game);
        tiles[maze.destination.Pos].SetVisited(true);

        for (int i = 0; i < game.mazeSize * game.mazeSize; i++)
        {
            Cell cell = maze.grid[i];
            tiles[cell.Pos].ReadCell(cell);
        }
        tiles[maze.origin.Pos].SetVisited(true);
    }

    IEnumerator DebugMaze()
    {
        InitTiles();
        tiles[maze.Init(game)].SetVisited(true);

        while (true)
        {
            int[] indices = maze.Step(); //get neighborIndex and currentIndex from step
            if (indices.Length < 1) break;

            tiles[indices[0]].MarkLocated(true);

            if (indices.Length > 1)
            {
                foreach (int position in indices)
                {
                    tiles[position].ReadCell(maze.grid[position]);
                }
            }

            yield return new WaitForSeconds(0.01f);
            tiles[indices[0]].MarkLocated(false);
        }


        Debug.Log("Maze generation finished");
    }

}
