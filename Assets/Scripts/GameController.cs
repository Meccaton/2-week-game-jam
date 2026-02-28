using UnityEngine;

public class GameController : MonoBehaviour
{
    public int state;

    void Start()
    {
        state = 0;
    }

    void Update()
    {
        if(state == 0) // new character walks in
        {

        }
        else if(state == 1) // character dialogue
        {

        }
        else if(state == 2) // coin choose phase
        {
            // TODO
        }
        else if(state == 3) // coin flip phase
        {

        }
    }
}
