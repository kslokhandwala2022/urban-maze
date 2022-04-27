using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class Game : ScriptableObject
{
    public int mazeSize;
    public int playerWealth;
    public float playerCommunity;
    public string playThrough;

    public float coinChance;
    public float fixableChance;
    public int numObjectsFixed;

    public int seed;
    public bool useSeed;
    public bool debug = true;

    public int speedTiers;
    public int speedWealth;
    public float speedMultiplier;

    public Color poorColor;
    public Color richColor;

    public int getSpeedTier()
    {
        return Mathf.Min(playerWealth / speedWealth, speedTiers);
    }

    public float getSpeedMultiplier()
    {
        return Mathf.Pow(speedMultiplier, getSpeedTier());
    }

    public Color getPlayerColor()
    {
        return Color.Lerp(poorColor, richColor, (float)getSpeedTier()/speedTiers);
    }

    public Color getPlayerEmmisiveColor()
    {
        return Color.Lerp(poorColor, richColor, (float)getSpeedTier() / speedTiers);
    }
}
