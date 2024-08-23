//System
using Michsky.UI.Dark;
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[DisallowMultipleComponent]
public class GameStarter : MonoBehaviour
{
    [Header("시작 안내 팝업")]
    [SerializeField] private ModalWindowManager startModalWindow;

    [Header("플레이어 이동 관련 컴포넌트")]
    [SerializeField] private PlayerMove playerMove;

    [Header("벽 움직임 관련 컴포넌트")]
    [SerializeField] private WallController wallController;

    private void Start()
    {
        //Modal Window In
        startModalWindow.ModalWindowIn();
    }

    public void GameStart()
    {
        //Modal Window Out
        startModalWindow.ModalWindowOut();

        //Convert Sign
        playerMove.canMove = true;
        wallController.gameStart = true;

        Cursor.visible = false;
    }
}
