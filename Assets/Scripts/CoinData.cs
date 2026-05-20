using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public static class CoinData
{
    public enum Modifier
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

    private static readonly Dictionary<string, Modifier> FaceModifiers = new()
    {
        {"Heads", Modifier.OnePoint},
        {"Star", Modifier.TwoPoints},
        {"Circle", Modifier.MinusOne},
        {"Square", Modifier.MinusTwo},
        {"Triangle", Modifier.Send},
        {"Swords", Modifier.Take},
        {"Lightning", Modifier.CancelSelf},
        {"Shield", Modifier.CancelOther},
        {"Heart", Modifier.Copy},
        {"Tails", Modifier.None}
    };

    private static readonly Dictionary<Modifier, int> ModifierScores = new()
    {
        {Modifier.OnePoint, 1},
        {Modifier.TwoPoints, 2},
        {Modifier.MinusOne, -1},
        {Modifier.MinusTwo, -2},
        {Modifier.None, 0}
    };

    public static Modifier GetModifier(string face, int pos)
    {
        if(face == "Sun")
        {
            switch(pos)
            {
                case 0:
                    return Modifier.TwoPoints;
                case 1:
                    return Modifier.OnePoint;
                default:
                    return Modifier.None;
            }
        }
        return FaceModifiers.TryGetValue(face, out var mod) ? mod : Modifier.None;
    }

    public static int GetScore(Modifier mod)
    {
        return ModifierScores.TryGetValue(mod, out int score) ? score : 0;
    }
}
