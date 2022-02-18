using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFSMaze : IMaze
{
    #region Variables

    const int WALL = 0;
    const int CLEAR = 1;
    const int CORRECT = 2;

    public Cell[] grid { get; set; }
    public int size;
    public Stack<Cell> stack;

    #endregion


    #region Required Functions
    public void GenerateMaze(int gridSize)
    {
        Init(gridSize);
        while (Step().Length > 0) ;
    }

    public int[] Step()
    {
        int[] res = new int[] { };
        if (stack.Count > 0)
        {
            Cell currentCell = stack.Pop();
            int neighbor = GetNeighbor(currentCell.Pos);
            if (neighbor > -1)//has a valid neighbor
            {
                stack.Push(currentCell);
                RemoveWall(currentCell, grid[neighbor]);
                grid[neighbor].isVisited = true;
                stack.Push(grid[neighbor]);
                res = new int[] { currentCell.Pos, neighbor };
            }
            else
            {
                res = new int[] { currentCell.Pos };
            }
        }
        return res;
    }

    public int Init(int gridSize)
    {
        grid = new Cell[gridSize * gridSize];
        size = gridSize;
        stack = new Stack<Cell>();

        for (int i = 0; i < gridSize * gridSize; i++)
        {
            grid[i] = new Cell(i, new int[] { WALL, WALL, WALL, WALL });
        }

        //initial cell, for now, 0, 0
        int initalPos = 35;
        Cell initial = grid[initalPos];
        initial.isVisited = true;
        stack.Push(initial);
        return initalPos;
    }

    #endregion


    #region Helper Functions
    //up right down left
    public void RemoveWall(Cell a, Cell b)
    {
        int diff = b.Pos - a.Pos;
        int wallToRemove = Mathf.Abs(diff) % size + (diff > 0 ? 2 : 0);


        if (Mathf.Abs(diff) == size)//up down
            wallToRemove = diff > 0 ? 2 : 0;
        if (Mathf.Abs(diff) == 1)//left right
            wallToRemove = diff > 0 ? 1 : 3;

        a.Walls[wallToRemove] = CLEAR;
        b.Walls[(wallToRemove + 2) % 4] = CLEAR;
    }

    //check neighbors, return wall to remove
    public int GetNeighbor(int currentPosition)
    {
        // up down +- size
        //left right +- 1
        List<int> walls = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            int neighborIndex = currentPosition + (i > 1 ? -1 : 1) * ((i + 1) % 2 * size + (i % 2));

            if (neighborIndex > -1 &&
                neighborIndex < grid.Length && //for array out of bounds edge cases
                ((neighborIndex + currentPosition + 1) % (2 * size) != 0) && //for the looping edge case (because I made this a 1D array)
                !grid[neighborIndex].isVisited)
            {
                walls.Add(neighborIndex);
            }
        }
        if (walls.Count > 0)
            return walls[Random.Range(0, walls.Count)];
        else
            return -1;
    }

    #endregion


    //NOtes for future
    /**
     * use primes to store cell data?
     * Anyway to store walls separately so I only have to edit one set of walls instead of two?
     * structs suck apparently in C#
     * 
     * 
     * 
     * 
     * 
     * */

}
