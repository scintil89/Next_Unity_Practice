using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public Texture2D pauseButtonImage;
    public static bool gamePause = false;

    void OnGUI()
    {
        int width = pauseButtonImage.width;
        int height = pauseButtonImage.height;

        float x = (Screen.width - width) - 10.0f;
        float y = 10.0f;
        Rect rect = new Rect(x, y, width, height);
        if (GUI.Button(rect, pauseButtonImage, GUIStyle.none))
        {
            gamePause = !gamePause;

            if (gamePause == true)
                Time.timeScale = 0.0f;
            else
                Time.timeScale = 1.0f;
        }
    }
}
