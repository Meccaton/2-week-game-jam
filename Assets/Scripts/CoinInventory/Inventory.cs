using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<CoinType> coins = new List<CoinType>();        // all owned coins
    public List<CoinType> equippedCoins = new List<CoinType>(); // up to 3 equipped

    public void AddCoin(CoinType coin)
    {
        if (!coins.Contains(coin))
            coins.Add(coin);
    }

    public void EquipCoin(CoinType coin)
    {
        if (!equippedCoins.Contains(coin) && equippedCoins.Count < 3)
            equippedCoins.Add(coin);
    }

    public void UnequipCoin(CoinType coin)
    {
        equippedCoins.Remove(coin);
    }

    public bool IsEquipped(CoinType coin) => equippedCoins.Contains(coin);
}