using UnityEngine;
using System.Collections.Generic;

public class CoinFlipControllerV2 : MonoBehaviour
{
    public int state;
    public bool movingUp;
    public Vector3 bottom;
    public Vector3 top;
    public GameObject cam;
    public Dictionary<string, int> chances;
    public string topFace = "Heads";
    public string botFace = "Tails";
    public Transform displayFace;
    public Transform previousFace;

    void Start()
    {
        state = 0;

        movingUp = true;

        bottom = new Vector3(0f, .5f, 0f);
        top = new Vector3(0f, 2f, 0f);

        chances = new Dictionary<string, int>();
        chances.Add("Blank", 0);   // placeholder; shouldnt ever show up
        chances.Add("Heads", 50);  // +1 score
        chances.Add("Tails", 50);  // 0 score
        chances.Add("Circle", 0);  // 
        chances.Add("Swords", 0);  // 
        chances.Add("Crown", 0);   // 
        chances.Add("Sun", 0);     // 
        chances.Add("Diamond", 0); // 
        chances.Add("Heart", 0);   // 
        chances.Add("Lighting", 0);// 
        chances.Add("Shield", 0);  // 
        chances.Add("Square", 0);  // 
        chances.Add("Rhombus", 0); // 
        chances.Add("Star", 0);    // 
        chances.Add("Triangle", 0);// 

        displayFace = transform.Find(topFace);
        displayFace.gameObject.SetActive(true);
        previousFace = transform.Find(botFace);
        previousFace.gameObject.SetActive(true);
        previousFace.localPosition = new Vector3(0f, .09f, 0f);
        previousFace.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    void Update()
    {
        cam.transform.LookAt(transform);
        cam.transform.rotation *= Quaternion.Euler(-20f, 0f, 0f);

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

    void Hold()
    {
        //cam.transform.position = Vector3.Lerp(cam.transform.position, originalPos, 2 * Time.deltaTime);

        if (Input.GetKeyDown("space"))
        {
            movingUp = true;
            
            state = 1;
        }
    }

    void Flip()
    {
        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, top, 2f * Time.deltaTime);

            if (transform.position.y >= 2f)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, bottom, 2f * Time.deltaTime);
        }

        transform.Rotate(3000f * Time.deltaTime, 0, 0);

        if (transform.position.y <= .5f && !movingUp)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            state = 2;
        }
    }

    void GetOutcome()
    {
        int total = 0;
        foreach(KeyValuePair<string, int> entry in chances)
        {
            total += entry.Value;
        }
        int rando = Random.Range(0, total);
        int runningTotal = 0;
        foreach(KeyValuePair<string, int> entry in chances)
        {
            runningTotal += entry.Value;
            if(rando < runningTotal)
            {
                DisplayFace(entry.Key);
                state = 3;
                break;
            }
        }
    }

    void WaitForInput()
    {
        if (Input.GetKeyDown("space"))
        {
            //displayFace.gameObject.SetActive(false);
            state = 0;
        }
    }

    void DisplayFace(string faceName)
    {
        

        if (faceName != topFace)
        {
            previousFace.gameObject.SetActive(false);
            previousFace.localPosition = new Vector3(0f, .1f, 0f);
            previousFace.rotation = Quaternion.Euler(-90f, 180f, 0f);

            botFace = topFace;
            previousFace = transform.Find(botFace);
            previousFace.localPosition = new Vector3(0f, .09f, 0f);
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
