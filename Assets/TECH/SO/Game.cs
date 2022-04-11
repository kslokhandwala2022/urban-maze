using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class Game : ScriptableObject
{
    public int mazeSize;
    public float playerWealth;
    public float playerCommunity;
    public string playThrough;

    public float coinChance;
    public float fixableChance;
    public int numObjectsFixed;

    public int seed;
    public bool useSeed;
    public bool debug = true;
}
