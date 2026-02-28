using UnityEngine;
using System.Collections.Generic;

public class CoinFlipControllerV2 : MonoBehaviour
{
    public bool flippingTime;
    public int state;
    //public GameObject cam;
    public List<CoinBehavior> coins;
    public List<OpponentCoinBehavior> opponentCoins;
    public int coinIdx;
    public List<string> winners = new();
    public List<string> opponentWinners = new();

    void Start()
    {
        flippingTime = false;
        state = 0;

        coinIdx = 0;

        //winners = new List<string>(3)
        //{
        //    null,
        //    null,
        //    null
        //};

        //opponentWinners = new List<string>(3)
        //{
        //    null,
        //    null,
        //    null
        //};
    }

    void Update()
    {
        if(flippingTime)
        {
            //cam.transform.LookAt(coins[coinIdx].transform);
            //cam.transform.rotation *= Quaternion.Euler(-15f, 0f, 0f);

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
        string playerOutcome = coins[coinIdx].CalculateOutcome();
        winners.Add(playerOutcome);
        string oppOutcome = opponentCoins[coinIdx].CalculateOutcome();
        opponentWinners.Add(oppOutcome);

        // IMPORTANT!!!!
        // do not add debug statements referencing winners[coinIdx] or opponentWinners[coinIdx] here like below
        // idk why but it adds random coins to the lists and breaks stuff down the line

        //if (winners[coinIdx] != null && opponentWinners[coinIdx] != null)
        //{
        //    state = 3;
        //}
        //Debug.Log("Player winner[" + coinIdx + "] = " + winners[coinIdx]);
        //Debug.Log("Opponent winner[" + coinIdx + "] = " +  opponentWinners[coinIdx]);
        state = 3;
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

    public void ResetResults()
    {
        winners.Clear();
        opponentWinners.Clear();
        foreach(CoinBehavior cb in coins)
        {
            cb.DisableText();
        }
        foreach (OpponentCoinBehavior ocb in opponentCoins)
        {
            ocb.DisableText();
        }
    }
}
