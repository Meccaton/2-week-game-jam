using UnityEngine;
using System.Collections.Generic;

public class CoinFlipControllerV2 : MonoBehaviour
{
    public bool flippingTime;
    public int state;
    public GameObject cam;
    public List<CoinBehavior> coins;
    public List<OpponentCoinBehavior> opponentCoins;
    public int coinIdx;
    public List<string> winners;
    public List<string> opponentWinners;

    void Start()
    {
        flippingTime = false;
        state = 0;

        coinIdx = 0;

        winners = new List<string>(3)
        {
            null,
            null,
            null
        };

        opponentWinners = new List<string>(3)
        {
            null,
            null,
            null
        };
    }

    void Update()
    {
        if(flippingTime)
        {
            cam.transform.LookAt(coins[coinIdx].transform);
            cam.transform.rotation *= Quaternion.Euler(-15f, 0f, 0f);

            if (state == 0)
            {
                Hold();
            }
            else if (state == 1)
            {
                Flip();
            }
            else if (state == 2)
            {
                GetOutcome();
            }
            else if (state == 3)
            {
                WaitForInput();
            }
        }
    }

    void Hold()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            winners[coinIdx] = null;
            opponentWinners[coinIdx] = null;
            coins[coinIdx].movingUp = true;
            opponentCoins[coinIdx].movingUp = true;
            
            state = 1;
        }
    }

    void Flip()
    {
        bool check = coins[coinIdx].FlipAnimation();
        opponentCoins[coinIdx].FlipAnimation();
        if (check)
        {
            state = 2;
        }
    }

    void GetOutcome()
    {
        winners[coinIdx] = coins[coinIdx].CalculateOutcome();
        opponentWinners[coinIdx] = opponentCoins[coinIdx].CalculateOutcome();
        if (winners[coinIdx] != null)
        {
            state = 3;
        }
    }

    void WaitForInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            coinIdx++;
            if(coinIdx > 2)
            {
                coinIdx = 0;
                flippingTime = false;
            }
            state = 0;
        }
    }
}
