using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed = 1.0f;
    public float mouseX = 0;
    public float mouseY = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseX += Input.GetAxis("Mouse Y") * -1;
        mouseX = Mathf.Clamp(mouseX, -45, 20);
        mouseY += Input.GetAxis("Mouse X");
        mouseY = Mathf.Clamp(mouseY, -45, 45);
        //transform.localRotation = Quaternion.Euler(mouseX, mouseY, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(mouseX, mouseY, 0), rotateSpeed * Time.deltaTime);
    }
}
