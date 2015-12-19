using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class saveManager : MonoBehaviour
{
    public static saveManager manager;
    public bool logProgress = false;
    private string loadData = "";
    public bool loading = false;
    public bool saving = false;
    public string gameName = "Game";
    public Rect windowRect = new Rect(20, 20, 120, 50);
    public string saveName = "";

    public void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if(manager != this)
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown("o"))
        {
            if(saving)
            {
                saving = false;
            }
            else
            {
                saving = true;
            }
        }
        if(Input.GetKeyDown("p"))
        {
            if(loading)
            {
                loading = false;
            }
            else
            {
                loading = true;
            }
        }
    }


    private void OnGUI()
    {
        if(saving)
        {
            GUIStyle box = "box";
            GUILayout.BeginArea(new Rect(Screen.width * 0.5f - 200.0f, Screen.height * 0.5f - 300.0f, 400.0f, 600.0f), box);

            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Save Game");
            if (GUILayout.Button("Save New Game"))
            {
                DateTime t = DateTime.Now;
                LevelSerializer.SaveGame(gameName);
                if (logProgress)
                {
                    Debug.Log(string.Format("Saved in: {0:0.000} seconds", (DateTime.Now - t).TotalSeconds));
                }
            }
            GUILayout.BeginHorizontal();
            GUILayout.Label("Save Name: ");
            gameName = GUILayout.TextField(gameName);
            GUILayout.EndHorizontal();
            GUILayout.Space(60f);
            GUILayout.Label("Click to overwrite existing saves");
            foreach (LevelSerializer.SaveEntry sg in LevelSerializer.SavedGames[LevelSerializer.PlayerName])
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(sg.Caption))
                {
                    saveName = sg.Name;
                    sg.Delete();
                    LevelSerializer.SaveGame(saveName);
                }

                if (GUILayout.Button("Delete Save"))
                {
                    sg.Delete();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        if (loading)
        {
            GUIStyle box = "box";
            GUILayout.BeginArea(new Rect(Screen.width * 0.5f - 200.0f, Screen.height * 0.5f - 300.0f, 400.0f, 600.0f), box);

            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Click save to load");
            GUILayout.Space(30f);
            foreach (LevelSerializer.SaveEntry sg in LevelSerializer.SavedGames[LevelSerializer.PlayerName])
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(sg.Caption))
                {
                    DateTime t = DateTime.Now;
                    LevelSerializer.LoadNow(sg.Data);
                    if (logProgress)
                    {
                        Debug.Log(string.Format("Loaded in: {0:0.000} seconds", (DateTime.Now - t).TotalSeconds));
                    }
                    Time.timeScale = 1.0f;
                    Time.fixedDeltaTime = Time.timeScale * 0.02f;
                    loading = false;
                }

                if (GUILayout.Button("Delete Save"))
                {
                    sg.Delete();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
