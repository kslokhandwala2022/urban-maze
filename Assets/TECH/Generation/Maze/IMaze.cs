using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMaze
{
    Cell[] grid { get; set; }
    Cell destination { get; set; }
    Cell origin { get; set; }

    int size { get; }
    public void GenerateMaze(Game game);

    public int[] Step();

    public int Init(Game game);
}
