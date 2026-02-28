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
    }
}
