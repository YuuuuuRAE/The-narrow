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
    private PlayerMove playerMove;

    private void Start()
    {
        gameManager = GameManager.instance;
        playerMove = FindObjectOfType<PlayerMove>();
    }

    public void Pause()
    {
        gameManager.Pause();

        playerMove.canMove = false;
    }

    public void Resume()
    {
        gameManager.Resume();

        playerMove.canMove = true;
    }
}
