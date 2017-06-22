using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour
{
    public GameObject BombObject;
    public GameObject BombObject2;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer.GetHashCode() == 0)
        {
            //Debug.Log("Collision Object Name : " + other.gameObject.name);
            Destroy(gameObject);

            GameObject obj = Instantiate(BombObject) as GameObject;
            obj.transform.position = gameObject.transform.position;
        }
        else if (other.gameObject.layer.GetHashCode() == 12)
        {
            //Debug.Log("Collision Object Name : " + other.gameObject.name);
            Destroy(gameObject);

            GameObject obj = Instantiate(BombObject2) as GameObject;
            obj.transform.position = gameObject.transform.position;
        }
    }
}
