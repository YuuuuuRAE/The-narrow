//System
using Michsky.UI.Dark;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

//Unity
using UnityEngine;

[DisallowMultipleComponent]
public class TutorialScene : MonoBehaviour
{
    //GameManager
    private GameManager gameManager;
    private PlayerMove playerMove;
    private Data data;

    private void Start()
    {
        gameManager = GameManager.instance;
        playerMove = FindObjectOfType<PlayerMove>();
        data = DataManager.Instance.data;
    }

    public void StageClear()
    {
        data.clear[0] = true;
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
