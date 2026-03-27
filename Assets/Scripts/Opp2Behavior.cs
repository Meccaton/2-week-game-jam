using UnityEngine;

public class Opp2Behavior : MonoBehaviour
{
    public Transform pink;
    public Transform blue;
    public Vector3 pinkTarget;
    public Vector3 blueTarget;
    float pinkX;
    float pinkY;
    float pinkZ;
    float blueX;
    float blueY;
    float blueZ;

    void Start()
    {
        //pink = transform.Find("Pink");
        //blue = transform.Find("Blue");
    }

    void Update()
    {
        pinkTarget = Random.insideUnitSphere * .1f;
        pinkTarget += pink.localPosition;
        pinkX = Mathf.Lerp(pink.position.x, pinkTarget.x, Time.deltaTime);
        pinkY = Mathf.Lerp(pink.position.y, pinkTarget.y, Time.deltaTime);
        pinkZ = Mathf.Lerp(pink.position.z, pinkTarget.z, Time.deltaTime);
        pink.localPosition = new Vector3(pinkX, pinkY, pinkZ);
        blueTarget = Random.insideUnitSphere * .1f;
        blueX = Mathf.Lerp(blue.position.x, blueTarget.x, Time.deltaTime);
        blueY = Mathf.Lerp(blue.position.y, blueTarget.y, Time.deltaTime);
        blueZ = Mathf.Lerp(blue.position.z, blueTarget.z, Time.deltaTime);
        blue.localPosition = new Vector3(blueX, blueY, blueZ);
    }
}
