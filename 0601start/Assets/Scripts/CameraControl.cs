using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour  
{
    public float sensitivity = 700.0f;
    float rotationX;
    float rotationY;

    PlayerState playerState = null;

    void Start()
    {
        playerState = transform.parent.GetComponent<PlayerState>();
    }

    void Update()
    {
        if (playerState.isDead)
            return;

        float mouseMoveValueX = Input.GetAxis("Mouse X");
        float mouseMoveValueY = Input.GetAxis("Mouse Y");

        rotationY += mouseMoveValueX * sensitivity * Time.deltaTime;
        rotationX += mouseMoveValueY * sensitivity * Time.deltaTime;

        rotationX %= 360;
        rotationY %= 360;

        if (rotationX >= 90)
        {
            rotationX = 90;
        }
        if (rotationX <= -90)
        {
            rotationX = -90;
            
        }

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0.0f);
    }
}
