//System
using Michsky.UI.Dark;
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[DisallowMultipleComponent]
public class TutorialScene : MonoBehaviour
{
    //GameManager
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    public void Pause()
    {
        gameManager.Pause();
    }

    public void Resume()
    {
        gameManager.Resume();
    }
}
