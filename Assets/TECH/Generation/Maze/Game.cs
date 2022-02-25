using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class Game : ScriptableObject
{
    public int mazeSize;
    public float wealth;
    public float community;
    public int seed;
    public bool useSeed;
    public bool debug = true;
}
