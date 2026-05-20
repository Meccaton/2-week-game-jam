using System.Collections.Generic;
using UnityEngine;

public class CoinEffectsController : MonoBehaviour
{
    public struct ArrowData
    {
        public Transform anchor;
        public Transform target;
    }

    public static ArrowData? ApplyMod(
        Dictionary<int, CoinData.Modifier> self,
        Dictionary<int, CoinData.Modifier> opp,
        int i,
        List<Transform> selfAnchors,
        List<Transform> oppAnchors)
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
            case CoinData.Modifier.Send:
                if (opp.ContainsKey(i) && self.ContainsKey(next))
                {
                    opp[i] = self[next];
                    self[next] = CoinData.Modifier.None;
                    return new ArrowData
                    {
                        anchor = selfAnchors[next],
                        target = opp[i]
                    };
                }
                break;
            case CoinData.Modifier.Take:
                self[i] = opp[i];
                opp[i] = CoinData.Modifier.None;
                return new ArrowData
                {
                    anchor = oppAnchors[i],
                    target = selfAnchors[i]
                };
            case CoinData.Modifier.CancelSelf:
                self[next] = CoinData.Modifier.None;
                return new ArrowData
                {
                    anchor = selfAnchors[i],
                    target = selfAnchors[next]
                };
            case CoinData.Modifier.CancelOther:
                opp[i] = CoinData.Modifier.None;
                return new ArrowData
                {
                    anchor = selfAnchors[i],
                    target = oppAnchors[i]
                };
            case CoinData.Modifier.Copy:
                self[i] = opp[i];
                return new ArrowData
                {
                    anchor = oppAnchors[i],
                    target = selfAnchors[i]
                };
            case CoinData.Modifier.Swap:
                // implement later
                break;
        }
        return null;
    }

    //void Start()
    //{
        
    //}

    //void Update()
    //{
        
    //}
}
