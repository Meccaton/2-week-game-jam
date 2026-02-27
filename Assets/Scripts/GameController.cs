using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;


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
    public List<string> playerCoins = new();
    public List<string> opponentCoins = new();
    public List<Modifiers> playerStack = new();
    public List<Modifiers> oppStack = new();
    //public int playerExtraIdx = 3;
    //public int oppExtraIdx = 3;

    public enum Modifiers
    {
        None,//implemented
        OnePoint,//implemented
        TwoPoints,//implemented
        MinusOne,//implemented
        MinusTwo,//implemented
        Send,//implemented
        Take,//implemented
        Swap,//need to figure out how exactly it will work
        CancelSelf,//implemented
        CancelOther,//implemented
        Copy,//implemented
        Reflip,//more complicated; implement later if time
        Protect,//more complicated; implement later if time
    }

    void Start()
    {
        state = 0;
        playerScore = 0;
        opponentScore = 0;
        round = 0;
        playerRecord = 0;
        opponentRecord = 0;
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
            cfc.ResetResults();
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
            //Debug.Log("Player has won this game");
        }
    }

    void CalcScore()
    {
        playerCoins.Clear();
        opponentCoins.Clear();

        playerStack.Clear();
        oppStack.Clear();

        playerScore = 0;
        opponentScore = 0;

        playerCoins = new List<string>(cfc.winners);
        opponentCoins = new List<string>(cfc.opponentWinners);

        //First pass to collect tags
        for(int i = 0; i < playerCoins.Count; i++)
        {
            CoinEffects(playerStack, playerCoins[i], i);
            CoinEffects(oppStack, opponentCoins[i], i);
        }

        //Second pass to apply reordering
        //Apply opponent's effects first
        for (int i = 0; i < Mathf.Max(playerStack.Count, oppStack.Count); i++)
        {
            if (i < oppStack.Count)
            {
                ReorderModifiers(oppStack, playerStack, oppStack[i], i);
            }

            if (i < playerStack.Count)
            {
                ReorderModifiers(playerStack, oppStack, playerStack[i], i);
            }
        }
        //Third pass to apply scoring
        for(int i = 0; i < Mathf.Max(playerStack.Count, oppStack.Count); i++)
        {
            if (i < oppStack.Count)
            {
                int temp = AddPoints(oppStack[i]);
                opponentScore += temp;
            }

            if(i < playerStack.Count)
            {
                int temp = AddPoints(playerStack[i]);
                playerScore += temp;
            }
        }

        if (playerScore > opponentScore)
        {
            playerRecord++;
            round++;
            state = 3;
            Debug.Log("Player has won this hand");
        }
        else if (opponentScore > playerScore)
        {
            opponentRecord++;
            round++;
            state = 3;
            Debug.Log("Opponent has won this hand");
        }
        else
        {
            state = 3;
            Debug.Log("Draw; replaying this hand");
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

    void CoinEffects(List<Modifiers> self, string coin, int i)
    {
        switch (coin)
        {
            case "Heads":
                self.Add(Modifiers.OnePoint);
                Debug.Log("Heads, 1 point");
                break;
            case "Star":
                self.Add(Modifiers.TwoPoints);
                break;
            case "Worm":
                if(i == 0)
                {
                    self.Add(Modifiers.TwoPoints);
                }
                else if(i == 1)
                {
                    self.Add(Modifiers.OnePoint);
                }
                else
                {
                    self.Add(Modifiers.None);
                }
                break;
            case "Tails":
                self.Add(Modifiers.None);
                break;
            case "a":
                self.Add(Modifiers.Send);
                break;
            case "b":
                self.Add(Modifiers.Take);
                break;
            case "c":
                self.Add(Modifiers.Swap);
                break;
            case "d":
                self.Add(Modifiers.CancelSelf);
                break;
            case "e":
                self.Add(Modifiers.CancelOther);
                break;
        }        
    }

    void ReorderModifiers(List<Modifiers> self, List<Modifiers> other, Modifiers mod, int i)
    {
        int next;
        if(i == 2)
        {
            next = 0;
        }
        else
        {
            next = i + 1;
        }

        switch (mod)
        {
            case Modifiers.Send:
                other[i] = self[next];
                self[next] = Modifiers.None;
                break;
            case Modifiers.Take:
                self.Insert(i, other[i]);
                self.RemoveAt(next);
                other[i] = Modifiers.None;
                break;
            case Modifiers.Swap:
                //TODO
                break;
            case Modifiers.CancelSelf:
                self[next] = Modifiers.None;
                break;
            case Modifiers.CancelOther:
                other[i] = Modifiers.None;
                break;
            case Modifiers.Copy:
                self[i] = other[i];
                break;
        }
    }

    int AddPoints(Modifiers val)
    {
        switch (val)
        {
            case Modifiers.OnePoint:
                return 1;
            case Modifiers.TwoPoints:
                return 2;
            case Modifiers.MinusOne:
                return -1;
            case Modifiers.MinusTwo:
                return -2;
            default:
                return 0;
        }
    }
}
