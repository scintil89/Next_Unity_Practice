using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject fireObject;
    public float forwardPower = 20.0f;
    public float upPower = 5.0f;

    public Transform firePosition;

    PlayerState playerState = null;

    void Start()
    {
        playerState = GetComponent<PlayerState>();
    }
        
    void Update()
    {
        if (PauseManager.gamePause)
            return;

        if (GUIUtility.hotControl != 0)
            return;

        if (playerState.isDead == true)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject obj = Instantiate(fireObject) as GameObject;

            obj.transform.position = firePosition.position;
            obj.GetComponent<Rigidbody>().velocity = cameraTransform.forward * forwardPower + Vector3.up * upPower;
            obj.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
        }
    }
}
