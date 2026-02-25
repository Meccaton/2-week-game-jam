using System.Collections.Generic;
using UnityEngine;

public class OpponentCoinBehavior : MonoBehaviour
{
    public bool movingUp;
    public Vector3 bottom;
    public Vector3 top;
    public string topFace = "Heads";
    public string botFace = "Tails";
    public Transform displayFace;
    public Transform previousFace;
    public Dictionary<string, int> chances;

    void Start()
    {
        movingUp = true;

        bottom = transform.position;
        top = transform.position + new Vector3(0f, 2f, 0f);

        displayFace = transform.Find(topFace);
        displayFace.gameObject.SetActive(true);
        previousFace = transform.Find(botFace);
        previousFace.gameObject.SetActive(true);
        previousFace.localPosition = new Vector3(0f, -.01f, 0f);
        previousFace.rotation = Quaternion.Euler(90f, 0f, 0f);

        chances = new Dictionary<string, int>
        {
            { "Blank", 0 },
            { "Heads", 50 },
            { "Tails", 50 },
            { "Circle", 0 },
            { "Swords", 0 },
            { "Crown", 0 },
            { "Sun", 0 },
            { "Diamond", 0 },
            { "Heart", 0 },
            { "Lighting", 0 },
            { "Shield", 0 },
            { "Square", 0 },
            { "Rhombus", 0 },
            { "Star", 0 },
            { "Triangle", 0 }
        };
    }

    void Update()
    {
        
    }

    public bool FlipAnimation()
    {
        if (movingUp)
        {
            transform.position = Vector3.Lerp(transform.position, top, 2f * Time.deltaTime);

            if (transform.position.y >= top.y - .2f)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, bottom, 2f * Time.deltaTime);
        }

        transform.Rotate(1000 * Time.deltaTime, 0, 0);

        if (transform.position.y <= .6f && !movingUp)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            return true;
        }
        return false;
    }

    public string CalculateOutcome()
    {
        int total = 0;
        foreach (KeyValuePair<string, int> entry in chances)
        {
            total += entry.Value;
        }
        int rando = Random.Range(0, total);
        int runningTotal = 0;
        foreach (KeyValuePair<string, int> entry in chances)
        {
            runningTotal += entry.Value;
            if (rando < runningTotal)
            {
                DisplayFace(entry.Key);
                return entry.Key;
            }
        }
        return null;
    }
    void DisplayFace(string faceName)
    {
        if (faceName != topFace)
        {
            previousFace.gameObject.SetActive(false);
            previousFace.localPosition = new Vector3(0f, 0f, 0f);
            previousFace.rotation = Quaternion.Euler(-90f, 180f, 0f);

            botFace = topFace;
            previousFace = transform.Find(botFace);
            previousFace.localPosition = new Vector3(0f, -.01f, 0f);
            previousFace.rotation = Quaternion.Euler(90f, 0f, 0f);

            topFace = faceName;

            //displayFace.gameObject.SetActive(false);

            displayFace = transform.Find(topFace);
            displayFace.gameObject.SetActive(true);
            Debug.Log("displayFace = " + displayFace);
            Debug.Log("previousFace = " + previousFace);
        }
    }
}
