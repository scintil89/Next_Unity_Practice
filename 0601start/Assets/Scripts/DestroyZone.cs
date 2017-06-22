using UnityEngine;
using System.Collections;

public class DestroyZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.GetHashCode() == 11)
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.layer.GetHashCode() == 10)
        {
            other.gameObject.transform.position = new Vector3(0, 100, 0);
        }
    }
}
