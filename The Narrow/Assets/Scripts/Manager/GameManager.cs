//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager instance;

    public bool isPause;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPause = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPause = false;
    }
}
