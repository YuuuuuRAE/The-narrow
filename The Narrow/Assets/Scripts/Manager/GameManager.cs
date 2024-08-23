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

    //Awake -> Start -> FixedUpdate -> Update -> LateUpdate
    // + OnEnable / OnDisable

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

    public bool isPause;

    public void Pause()
    {
        Cursor.visible = true;

        Time.timeScale = 0f;
        isPause = true;
    }

    public void Resume()
    {
        Cursor.visible = false;

        Time.timeScale = 1f;
        isPause = false;
    }
}
