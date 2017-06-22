using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SimpleClass))]

public class SimpleClassEditor : Editor
{
    bool myListFold = false;

    public override void OnInspectorGUI()
    {
        SimpleClass simpleClass = target as SimpleClass;

        simpleClass.showButton = EditorGUILayout.Toggle("Show Button", simpleClass.showButton);

        if (simpleClass.showButton)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Show button") == true)
            {
                Debug.LogWarning("================ Show Button ================");
            }

            if (GUILayout.Button("Right button") == true)
            {
                Debug.Log("================ Right Button ================");
            }

            if (GUILayout.Button("3 button") == true)
            {
                Debug.Log("================ 3 Button ================");
            }

            if (GUILayout.Button("4 button") == true)
            {
                Debug.Log("================ 4 Button ================");
            }

            if (GUILayout.Button("5 button") == true)
            {
                Debug.Log("================ 5 Button ================");
            }

            EditorGUILayout.EndHorizontal();
        }


        MyEditorUtility.DrawSeparator(Color.black);

        int level = EditorGUILayout.IntField("Level", simpleClass.level);
        if(level != simpleClass.level)
        {
            MyEditorUtility.RecordObject(simpleClass, "Level Change");
            simpleClass.level = level;
        }

        //simpleClass.nickName = EditorGUILayout.TextField("Weight", simpleClass.nickName);
        simpleClass.heroType = (HEROTYPE)EditorGUILayout.EnumPopup("Hero Type", simpleClass.heroType);

        simpleClass.mainCameraObject = (GameObject)EditorGUILayout.ObjectField(
            "Camera Object", simpleClass.mainCameraObject, typeof(GameObject), false);

        simpleClass.myTransform = (Transform)EditorGUILayout.ObjectField(
            "My Transform", simpleClass.myTransform, typeof(Transform), true);


        MyEditorUtility.DrawSeparator(Color.black);


        if (myListFold = EditorGUILayout.Foldout(myListFold, "My Vec3"))
        {
            for (int i = 0; i != simpleClass.arrayVector3.Length; ++i)
            {
                simpleClass.arrayVector3[i] = EditorGUILayout.Vector3Field(
                  "arrayVector3", simpleClass.arrayVector3[i]);
            }
        }

        MyEditorUtility.DrawSeparator(new Color(1.0f, 0.0f, 1.0f));

        if (GUILayout.Button("Create File"))
        {
            string dataPath = Application.dataPath; //pc : asset 폴더 까지의 경로
                                                    //Application.persistentDataPath

            string fullPath = dataPath + "/test.txt";

            FileStream fs = new FileStream(fullPath, FileMode.Create);

            TextWriter textWriter = new StreamWriter(fs);

            textWriter.Write("width : 10" + "\n");

            textWriter.Close();

            // copy
            string copyPath = dataPath += "/test_copy.txt";
            FileUtil.CopyFileOrDirectory(fullPath, copyPath);

            AssetDatabase.Refresh();
        }

        SerializedProperty iterator = serializedObject.FindProperty("weight");
        EditorGUILayout.PropertyField(iterator);
        serializedObject.ApplyModifiedProperties();
    }
}
