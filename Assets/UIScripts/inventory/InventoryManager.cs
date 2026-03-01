using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Coin> allCoins = new List<Coin>();      // full inventory
    public List<Coin> equippedCoins = new List<Coin>(); // max 3

    public bool EquipCoin(Coin coin)
    {
        if (equippedCoins.Contains(coin))
        {
            equippedCoins.Remove(coin); // unequip
            return true;
        }

        if (equippedCoins.Count >= 3)
        {
            Debug.Log("Cannot equip more than 3 coins!");
            return false;
        }

        equippedCoins.Add(coin);
        return true;
    }

    public List<string> GetEquippedCoinNames()
    {
        List<string> names = new List<string>();
        foreach (var coin in equippedCoins)
            names.Add(coin.name); // pass to gameplay logic
        return names;
    }
}