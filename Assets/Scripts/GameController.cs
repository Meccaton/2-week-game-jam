using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


public class GameController : MonoBehaviour
{
    public int state;
    public int playerScore;
    public int opponentScore;
    public int round;
    public int maxRounds;
    public int playerRecord;
    public int opponentRecord;
    public CoinFlipControllerV2 cfc;
    public List<string> playerCoins;
    public List<string> opponentCoins;

    void Start()
    {
        state = 0;
        playerScore = 0;
        opponentScore = 0;
        round = 0;
        playerRecord = 0;
        opponentRecord = 0;

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
        else if (state == 5) // acquire coin data, determine winner
        {
            CalcScore();
        }
        else if(state == 6)
        {
            Debug.Log("Player has won this game");
        }
    }

    void CalcScore()
    {
        playerScore = 0;
        opponentScore = 0;

        playerCoins = cfc.winners;
        opponentCoins = cfc.opponentWinners;

        foreach (string c in playerCoins)
        {
            if (c == "Heads")
            {
                playerScore++;
            }
        }

        foreach (string c in opponentCoins)
        {
            if (c == "Heads")
            {
                opponentScore++;
            }
        }

        if (playerScore > opponentScore)
        {
            playerRecord++;
            round++;
            state = 3;
            Debug.Log("Player won this round");
        }
        else if (opponentScore > playerScore)
        {
            opponentRecord++;
            round++;
            state = 3;
            Debug.Log("Opponent won this round");
        }
        else
        {
            state = 3;
            Debug.Log("Draw; replaying this round");
        }

        if (round >= maxRounds)
        {
            if (playerScore > opponentScore)
            {
                state = 6;
                Debug.Log("Player wins");
            }
            else if (opponentScore > playerScore)
            {
                state = 6;
                Debug.Log("Opponent wins");
            }
            else
            {
                Debug.Log("How did you get here?");
            }
        }
    }
}
