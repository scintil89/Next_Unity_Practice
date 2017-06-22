using UnityEngine;
using System.Collections.Generic;

public enum HEROTYPE
{
    KNIGHT = 0,
    FIGHTER,
    MONK,
};


public class SimpleClass : MonoBehaviour
{
    public bool showButton = true;
    public int level = 1;
    public float weight = 52.3f;
    public string nickName = "Kevin";
    public HEROTYPE heroType = HEROTYPE.KNIGHT;

    public GameObject mainCameraObject;
    public Transform myTransform;

    public List<int> myList;
    public float[] arrayFloat;
    public Vector3[] arrayVector3;
}
