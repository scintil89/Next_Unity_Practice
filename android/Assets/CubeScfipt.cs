using UnityEngine;
using System.Collections;

public class CubeScfipt : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	
	}

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * 500);
        transform.Rotate(Vector3.up * Time.deltaTime * 500, Space.World);
    }
}
