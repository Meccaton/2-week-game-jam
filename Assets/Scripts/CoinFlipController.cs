using UnityEngine;

public class CoinFlipController : MonoBehaviour
{
    // 1 = pre-flip, 2 = mid-flip, 3 = post-flip
    public int state;
    public bool movingUp;
    public Vector3 top;
    public Vector3 bot;
    public float moveSpeed = 1.0f;
    public GameObject cam;
    public Vector3 originalPos;

    void Start()
    {
        state = 0;
        transform.position = new Vector3(0, .05f, 0);
        transform.Rotate(90.0f, 0, 0);
        transform.rotation = Quaternion.Euler(90, 0, 0);
        movingUp = true;
        top = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        bot = transform.position;
        originalPos = cam.transform.position;
    }

    void Update()
    {
        if(state == 0)
        {
            Hold();
        }
        else if(state == 1)
        {
            Flip();
        }
        else if(state == 2)
        {
            GetOutcome();
        }
        else if(state == 3)
        {
            WaitForInput();
        }
    }

    void Hold()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, originalPos, 2 * Time.deltaTime);
        //cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.Euler(0f, 0f, 0f), 1 * Time.deltaTime);
        cam.transform.LookAt(transform);

        if (Input.GetKeyDown("space"))
        {
            state = 1;
        }
    }

    void Flip()
    {
        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, top, moveSpeed * Time.deltaTime);
            //cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.Euler(-30f, 0f, 0f), .9f * Time.deltaTime);
            cam.transform.LookAt(transform);

            if (transform.position.y >= top.y)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, bot, moveSpeed * Time.deltaTime);
            //cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.Euler(0f, 0f, 0f), 1 * Time.deltaTime);
            cam.transform.LookAt(transform);
        }
        transform.Rotate(1200.0f * Time.deltaTime, 0, 0);

        if(transform.position.y < .06f && !movingUp)
        {
            state = 2;
        }
    }

    void GetOutcome()
    {
        
        int outcome = Random.Range(0, 2);
        if(outcome == 0)
        {
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(270, 0, 180);
        }

        state = 3;
    }

    void WaitForInput()
    {
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(0f, .5f, 0f), 2 * Time.deltaTime);
        //cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.Euler(90f, 0f, 0f), 1 * Time.deltaTime);
        cam.transform.LookAt(transform);

        movingUp = true;

        if(Input.GetKeyDown("space"))
        {
            state = 0;
        }
    }
}
