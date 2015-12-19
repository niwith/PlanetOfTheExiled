using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class mainMenuGUI : MonoBehaviour
{
    public Color backgroundColor = new Color(0,0,0,0);
    public Vector2 backgroundSize;
    public Vector2 buttonSize;
    public GUIStyle styleBackground;
    public String menu = "main";
    public string gameName = "Your Game";
    public static bool logProgress = false;

    public void Start()
    {
        styleBackground.normal.background = MakeTex(2, 2, backgroundColor);
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    public void OnGUI()
    {
        GUI.Box(new Rect((Screen.width / 2) - (backgroundSize.x / 2), (Screen.height / 2) - (backgroundSize.y / 2), backgroundSize.x, backgroundSize.y), "", styleBackground);
        
        if (menu == "main")
        {
            if (GUI.Button(new Rect((Screen.width / 2) - (buttonSize.x / 2), (Screen.height / 2) - (buttonSize.y / 2) - (buttonSize.y + buttonSize.y/2), buttonSize.x, buttonSize.y), "Start New Game"))
            {
                SceneManager.LoadScene(1);
            }

            if (GUI.Button(new Rect((Screen.width / 2) - (buttonSize.x / 2), (Screen.height / 2) - (buttonSize.y / 2), buttonSize.x, buttonSize.y), "Load Save"))
            {
                SceneManager.LoadScene(1);
                saveManager.manager.loading=true;
            }
        }
    }
}
