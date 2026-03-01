using UnityEngine;
using TMPro;

public class CoinTextController : MonoBehaviour
{
    public TMP_Text faceName;
    public Transform canvas;
    public Transform player;

    void Start()
    {
        faceName.enabled = false;
        canvas = transform.Find("Canvas");
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        canvas.LookAt(player);
        canvas.rotation *= Quaternion.Euler(-1, 180, 0);
    }

    void Update()
    {
        
    }

    public void EnableText()
    {
        faceName.enabled = true;
    }

    public void DisableText()
    {
        faceName.enabled = false;
    }

    public void ChangeText(string text)
    {
        faceName.text = text;

        switch (text)
        {
            case "Heads":
                faceName.text = "+1!";
                break;
            case "OnePoint":
                faceName.text = "+1!";
                break;
            case "Star":
                faceName.text = "+2!";
                break;
            case "TwoPoints":
                faceName.text = "+2!";
                break;
            case "Tails":
                faceName.text = "0";
                break;
            case "None":
                faceName.text = "0";
                break;
            case "Circle":
                faceName.text = "-1!";
                break;
            case "MinusOne":
                faceName.text = "-1!";
                break;
            case "Square":
                faceName.text = "-2!";
                break;
            case "MinusTwo":
                faceName.text = "-2!";
                break;
            case "Swords":
                faceName.text = "STEAL!";
                break;
            case "Take":
                faceName.text = "STEAL!";
                break;
            case "Sun":
                faceName.text = "EARLY BIRD!";
                break;
            case "Lightning":
                faceName.text = "STRIKE!";
                break;
            case "CancelSelf":
                faceName.text = "STRIKE!";
                break;
            case "Shield":
                faceName.text = "BLOCK!";
                break;
            case "CancelOther":
                faceName.text = "BLOCK!";
                break;
            case "Triangle":
                faceName.text = "SEND!";
                break;
            case "Send":
                faceName.text = "SEND!";
                break;
            case "Heart":
                faceName.text = "COPY!";
                break;
            case "Copy":
                faceName.text = "COPY!";
                break;
        }
    }
}
