using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


public class GameController : MonoBehaviour
{
    public int state;
    public CoinFlipControllerV2 cfc;
    public List<string> playerCoins;
    public List<string> opponentCoins;

    void Start()
    {
        state = 0;
        playerCoins = new List<string>(3)
        {
            null,
            null,
            null
        };
        opponentCoins = new List<string>(3)
            {
            null,
            null,
            null
        };
    }

    void Update()
    {
        if(state == 0) // new character walks in
        {
            // TODO
            state = 1;
        }
        else if(state == 1) // character dialogue
        {
            // TODO
            state = 2;
        }
        else if(state == 2) // coin choose phase
        {
            // TODO
            state = 3;
        }
        else if(state == 3) // coin flip phase
        {
            cfc.flippingTime = true;
            state = 4;
        }
        else if(state == 4)
        {
            if (!cfc.flippingTime) // wait for flipping to complete
            {
                state = 5;
            }
        }
        else if (state == 5)
        {
            playerCoins = cfc.winners;
            opponentCoins = cfc.opponentWinners;
            Debug.Log("Player coins: " + playerCoins[0] + ", " + playerCoins[1] + ", " + playerCoins[2]);
            Debug.Log("Opponent coins: " + opponentCoins[0] + ", " + opponentCoins[1] + ", " + opponentCoins[2]);
        }
    }
}
