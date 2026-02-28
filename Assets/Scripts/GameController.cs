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
    public Dictionary<int, string> pCoins = new();
    public Dictionary<int, string> oppCoins = new();
    public Dictionary<int, Modifiers> pStack = new();
    public Dictionary<int, Modifiers> oStack = new();
    public int pArrowIdx;
    public int oppArrowIdx;
    public List<GameObject> pArrows = new();
    public List<GameObject> oppArrows = new();
    public Transform pCurAnchor;
    public Transform oppCurAnchor;
    public Transform pCurTarget;
    public Transform oppCurTarget;
    public List<Transform> playerAnchors = new();
    public List<Transform> oppAnchors = new();
    public int coinOrganizingIdx = 0;

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
            CollectTags();
            coinOrganizingIdx = 0;
            state = 6;
        }
        else if(state == 6)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(coinOrganizingIdx < 3)
                {
                    Reorganize(coinOrganizingIdx);
                    coinOrganizingIdx++;
                }
                else
                {
                    state = 7;
                }
                
            }
        }
        else if(state == 7)
        {
            ApplyScoring();
        }
        else if (state == 8) // pause
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                state = 9;
            }
        }
        else if (state == 9)
        {
            //TBD
        }
    }

    void CollectTags()
    {
        pCoins.Clear();
        oppCoins.Clear();

        pStack.Clear();
        oStack.Clear();

        playerScore = 0;
        opponentScore = 0;

        pArrowIdx = 0;
        oppArrowIdx = 0;

        int uh = 0;
        foreach (string s in cfc.winners)
        {
            pCoins[uh] = s;
            //Debug.Log("Player coin " + uh + " is " + pCoins[uh]);
            uh++;
        }
        uh = 0;
        foreach (string s in cfc.opponentWinners)
        {
            oppCoins[uh] = s;
            //Debug.Log("Opp coin " + uh + " is " + oppCoins[uh]);
            uh++;
        }

        //First pass to collect tags
        for (int i = 0; i < pCoins.Count; i++)
        {
            CoinEffects(pStack, pCoins[i], i);
            CoinEffects(oStack, oppCoins[i], i);
        }
    }

    void Reorganize(int i)
    {
        if (i < pStack.Count)
        {
            ReorderModifiers(pStack, oStack, i, true);
            if (pCurAnchor && pCurTarget)
            {
                PlaceArrow(true);
            }
            pArrowIdx++;
        }

        if (i < oStack.Count)
        {
            ReorderModifiers(oStack, pStack, i, false);
            if (oppCurAnchor && oppCurTarget)
            {
                PlaceArrow(false);
            }
            oppArrowIdx++;
        }
    }

    void ApplyScoring()
    {
        //Third pass to apply scoring
        for (int i = 0; i < Mathf.Max(pStack.Count, oStack.Count); i++)
        {
            if (i < pStack.Count)
            {
                int temp = AddPoints(pStack[i]);
                playerScore += temp;
            }

            if (i < oStack.Count)
            {
                int temp = AddPoints(oStack[i]);
                opponentScore += temp;
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

        //Reset arrows
        foreach(GameObject arrow in pArrows)
        {
            arrow.SetActive(false);
        }
        foreach(GameObject arrow in oppArrows)
        {
            arrow.SetActive(false);
        }


        if (round >= maxRounds)
        {
            if (playerRecord > opponentRecord)
            {
                state = 6;
                Debug.Log("Player wins");
            }
            else if (opponentRecord > playerRecord)
            {
                state = 6;
                Debug.Log("Opponent wins");
            }
            else
            {
                Debug.Log("How did you manage to reach a tie in a best of 3?");
            }
        }
    }

    void CoinEffects(Dictionary<int, Modifiers> self, string coin, int i)
    {
        switch (coin)
        {
            case "Heads":
                self[i] = Modifiers.OnePoint;
                //Debug.Log("Heads, 1 point");
                break;
            case "Star":
                self[i] = Modifiers.TwoPoints;
                //Debug.Log("Star, 2 points");
                break;
            case "Circle":
                self[i] = Modifiers.MinusOne;
                //Debug.Log("Circle, minus 1 point");
                break;
            case "Square":
                self[i] = Modifiers.MinusTwo;
                //Debug.Log("Square, minus 2 points");
                break;
            case "Sun"://change to Worm later
                //Debug.Log("Sun, early is better");
                if(i == 0)
                {
                    self[i] = Modifiers.TwoPoints;
                }
                else if(i == 1)
                {
                    self[i] = Modifiers.OnePoint;
                }
                else
                {
                    self[i] = Modifiers.None;
                }
                break;
            case "Tails":
                self[i] = Modifiers.None;
                break;
            case "Triangle":
                self[i] = Modifiers.Send;
                //Debug.Log("Triangle, send a coin");
                break;
            case "Swords":
                self[i] = Modifiers.Take;
                //Debug.Log("Swords, take a coin");
                break;
            //case "Swap":
            //    self[i] = Modifiers.Swap;
            //    break;
            case "Lightning":
                self[i] = Modifiers.CancelSelf;
                //Debug.Log("Lightning, cancel next coin");
                break;
            case "Shield":
                self[i] = Modifiers.CancelOther;
                //Debug.Log("Shield, cancel opposing coin");
                break;
        }        
    }

    void ReorderModifiers(Dictionary<int, Modifiers> self, Dictionary<int, Modifiers> other, int i, bool player)
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

        switch (self[i])
        {
            case Modifiers.Send:
                if(other.ContainsKey(i) && self.ContainsKey(next))
                {
                    other[i] = self[next];
                    self[next] = Modifiers.None;
                        if (player)
                        {
                            pCurAnchor = playerAnchors[next];
                            pCurTarget = oppAnchors[i];
                        }
                        else
                        {
                            oppCurAnchor = oppAnchors[next];
                            oppCurTarget = playerAnchors[i];
                        }
                }
                break;
            case Modifiers.Take:
                self[i] = other[i];
                other[i] = Modifiers.None;
                if (player)
                {
                    pCurAnchor = oppAnchors[i];
                    pCurTarget = playerAnchors[i];
                }
                else
                {
                    oppCurAnchor = playerAnchors[i];
                    oppCurTarget = oppAnchors[i];
                }
                break;
            case Modifiers.Swap:
                //TODO
                break;
            case Modifiers.CancelSelf:
                self[next] = Modifiers.None;
                if (player)
                {
                    pCurAnchor = playerAnchors[i];
                    pCurTarget = playerAnchors[next];
                }
                else
                {
                    oppCurAnchor = oppAnchors[i];
                    oppCurTarget = oppAnchors[next];
                }
                break;
            case Modifiers.CancelOther:
                other[i] = Modifiers.None;
                if (player)
                {
                    pCurAnchor = playerAnchors[i];
                    pCurTarget = oppAnchors[i];
                }
                else
                {
                    oppCurAnchor = oppAnchors[i];
                    oppCurTarget = playerAnchors[i];
                }
                break;
            case Modifiers.Copy:
                self[i] = other[i];
                if (player)
                {
                    pCurAnchor = oppAnchors[i];
                    pCurTarget = playerAnchors[i];
                }
                else
                {
                    oppCurAnchor = playerAnchors[i];
                    oppCurTarget = oppAnchors[i];
                }
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

    void PlaceArrow(bool player)
    {
        if (player)
        {
            pArrows[pArrowIdx].transform.position = (pCurAnchor.position + pCurTarget.position) / 2;
            pArrows[pArrowIdx].transform.LookAt(pCurTarget.position);            pArrows[pArrowIdx].SetActive(true);
            Debug.Log("Activated player arrow");

            
        }
        else
        {
            oppArrows[oppArrowIdx].transform.position = (oppCurAnchor.position + oppCurTarget.position) / 2;
            oppArrows[oppArrowIdx].transform.LookAt(oppCurTarget.position);
            oppArrows[oppArrowIdx].SetActive(true);
            Debug.Log("Activated opp arrow");

            
        }
    }
}
