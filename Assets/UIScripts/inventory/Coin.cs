using UnityEngine;

[System.Serializable]
public class Coin
{
    public string name;           // Must match the name used in CoinBehavior (Heads, Tails, etc.)
    public GameObject prefab;     // The Coin GameObject prefab to spawn in the world
    public string description;    // Optional: for inventory UI
}