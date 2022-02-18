using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMaze
{
    Cell[] grid { get; set; }
    public void GenerateMaze(int gridSize);

    public int[] Step();

    public int Init(int gridSize);
}
