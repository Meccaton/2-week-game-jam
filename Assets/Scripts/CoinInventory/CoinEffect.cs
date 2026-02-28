using UnityEngine;

[System.Serializable]
public class CoinEffect
{
    public string effectName;
    [TextArea]
    public string description;
    [Range(0, 100)]
    public int weight; // percentage weight of this effect
}