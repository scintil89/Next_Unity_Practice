using UnityEngine;
using UnityEditor;
using System.Collections;

//[CustomEditor(typeof(SimpleClass))]

public class MyWindow : EditorWindow
{
    SimpleClass simpleClass;

    enum UIMODE
    {
        MODE1 = 0,
        MODE2
    }
    UIMODE uiMode = UIMODE.MODE1;

    bool isShow = false;
    Texture texture;



    [MenuItem("MyMenu/Create Window")]
    static void CreateWindow()
    {
        MyWindow window = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow));
    }

    void OnEnable()
    {
        SceneView.onSceneGUIDelegate += SceneGUI;
    }

    void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= SceneGUI;
    }

    void ShowMyWindow(int windowID)
    {
        GUILayout.Label("Test Lable =================");

        GUILayout.BeginHorizontal();
        GUILayout.Button("Button 1");
        GUILayout.Button("Button 2");
        GUILayout.EndHorizontal();

        Rect rect = new Rect(10, 80, 100, 100);

        if(GUI.Button(rect, texture))
        {
            Debug.Log("Click Texture");
        }

        rect.x += 110;

        if(GUI.Button(rect, "Text"))
        {
            Debug.LogWarning("Click Text Button");
        }

    }

    void SceneGUI(SceneView sceneView)
    {   
        //same as OnSceneGUI    

        Event e = Event.current;


        Handles.BeginGUI();
        
        switch(uiMode)
        {
            case UIMODE.MODE1:
                {
                    if (GUI.Button(new Rect(10, 10, 100, 50), "toMODE2"))
                        uiMode = UIMODE.MODE2;
                }
                break;

            case UIMODE.MODE2:
                {
                    if (GUI.Button(new Rect(10, 10, 100, 50), "toMODE1"))
                        uiMode = UIMODE.MODE1;

                    if (GUI.Button(new Rect(170, 10, 200, 50), "Show My Window"))
                    {
                        Debug.Log("Hello==========================");
                        isShow = !isShow;
                    }

                    if (isShow)
                        GUILayout.Window(0, new Rect(10, 90, 300, 300), ShowMyWindow, "Unity Objects");
                }
                break;
        }

   
        Handles.EndGUI();
        //Handles.SphereCap(0, Vector3.zero, Quaternion.identity, 1.0f);

    }

    void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.Label("This is MyFirst Window!", GUILayout.MinWidth(20));
        GUILayout.Space(10);

        simpleClass = (SimpleClass)EditorGUILayout.ObjectField("simpleClass", simpleClass, typeof(SimpleClass), true);

        EditorGUILayout.BeginHorizontal();
        //white
        if (!simpleClass)
            return;

            simpleClass.heroType = (HEROTYPE)EditorGUILayout.EnumPopup("Hero Type", simpleClass.heroType);

        GUI.color = Color.green;
        GUILayout.Button("Add");

        GUI.color = Color.red;
        GUILayout.Button("X");

        GUI.color = Color.white;

        EditorGUILayout.EndHorizontal();

        //Percent
        int level = EditorGUILayout.IntField("Level", simpleClass.level);
        if (level != simpleClass.level)
        {
            MyEditorUtility.RecordObject(simpleClass, "Level Change");
            simpleClass.level = level;
        }


        //Close Window
        if ( GUILayout.Button("Close") == true ) 
        {
             this.Close();
        }
    }



}
