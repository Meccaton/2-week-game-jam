using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "NewCoinType", menuName = "Inventory/CoinType")]
public class CoinType : ScriptableObject
{
    public string coinName;
    public Sprite icon;
    public List<CoinEffect> effects = new List<CoinEffect>();

    // Pick a random effect based on weights
    public CoinEffect GetRandomEffect()
    {
        int totalWeight = 0;
        foreach (var effect in effects)
            totalWeight += effect.weight;

        int randomValue = UnityEngine.Random.Range(0, totalWeight);
        int currentSum = 0;

        foreach (var effect in effects)
        {
            currentSum += effect.weight;
            if (randomValue < currentSum)
                return effect;
        }

        // Fallback (shouldn't happen if weights are correct)
        return effects.Count > 0 ? effects[0] : null;
    }
}