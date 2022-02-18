using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DFSMaze
{
    const int WALL = 0;
    const int CLEAR = 1;
    const int CORRECT = 2;

    public class Cell
    {
        public Cell(int position, int[] walls)
        {
            Walls = walls;
            Pos = position;
            isVisited = false;
        }
        public int[] Walls;
        public int Pos;
        public bool isVisited;

        //check neighbors, return wall to remove
        public int getNeighbor()
        {
            // up down +- size
            //left right +- 1
            List<int> walls = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                int neighborIndex = Pos + (i > 1 ? -1 : 1) * ( (i + 1) % 2 * size + (i % 2) );
                
                if (neighborIndex > 0 && 
                    neighborIndex < grid.Length && //for array out of bounds edge cases
                    ((neighborIndex + Pos + 1) % (2 * size) != 0) && //for the looping edge case (because I made this a 1D array)
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
    }

    public static Cell[] grid;
    public static int size;
    public static Stack<Cell> stack;


    public static void generateMaze(int gridSize)
    {
        size = gridSize;
        init(gridSize);

        //initial cell, for now, 0, 0
        Cell initial = grid[0];
        initial.isVisited = true;
        stack.Push(initial);

        Cell currentCell;
        int neighbor;

        while (stack.Count > 0) {

            currentCell = stack.Pop();
            neighbor = currentCell.getNeighbor();
            if(neighbor > 0)//has a valid neighbor
            {

                stack.Push(currentCell);
                removeWall(currentCell, grid[neighbor]);
                grid[neighbor].isVisited = true;
                stack.Push(grid[neighbor]);
            }
        }
    }

    public static void init(int gridSize)
    {
        grid = new Cell[gridSize * gridSize];
        stack = new Stack<Cell>();
        for (int i = 0; i < gridSize * gridSize; i++)
        {
            grid[i] = new Cell(i, new int[] { WALL, WALL, WALL, WALL });
        }

    }

    //up right down left
    public static void removeWall(Cell a, Cell b)
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
