using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour
{	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 50);
	
	}


    void OnGUI()
    {
        if(GUI.Button( new Rect(100, 100, Screen.width*0.1f, Screen.width*0.1f), "launch") )
        {
            if (Application.platform != RuntimePlatform.Android)
                return;

            //Load Package
            AndroidJavaClass pluginClass = new AndroidJavaClass("com.dz.DzPlugin");

            //Get class instance
            AndroidJavaObject _plugin = pluginClass.CallStatic<AndroidJavaObject>("instance");

            //call Java Method
            _plugin.Call("ShowToast", "Test Tost 2", true);
        }
    }




}
