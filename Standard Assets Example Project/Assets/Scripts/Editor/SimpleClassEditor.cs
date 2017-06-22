using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SimpleClass))]

public class SimpleClassEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SimpleClass simpleClass = target as SimpleClass;

        simpleClass.showButton = EditorGUILayout.Toggle("Show Button", simpleClass.showButton);

        if (simpleClass.showButton)
        {
            if (GUILayout.Button("Show button") == true)
            {
                Debug.Log("================ Show Button ================");
            }
        }


        simpleClass.level = EditorGUILayout.IntField("Level", simpleClass.level);
        simpleClass.weight = EditorGUILayout.FloatField("Weight", simpleClass.weight);
        simpleClass.nickName = EditorGUILayout.TextField("Weight", simpleClass.nickName);
        simpleClass.heroType = (HEROTYPE)EditorGUILayout.EnumPopup("Hero Type", simpleClass.heroType);

        simpleClass.mainCameraObject = (GameObject)EditorGUILayout.ObjectField(
            "Camera Object", simpleClass.mainCameraObject, typeof(GameObject), false);

        simpleClass.myTransform = (Transform)EditorGUILayout.ObjectField(
            "My Transform", simpleClass.myTransform, typeof(Transform), true);


        for (int i = 0; i != simpleClass.arrayVector3.Length; ++i)
        {
            simpleClass.arrayVector3[i] = EditorGUILayout.Vector3Field(
              "arrayVector3", simpleClass.arrayVector3[i]);
        }

    }
}
