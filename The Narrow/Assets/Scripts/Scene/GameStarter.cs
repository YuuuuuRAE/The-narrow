//System
using Michsky.UI.Dark;
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[DisallowMultipleComponent]
public class GameStarter : MonoBehaviour
{
    [Header("���� �ȳ� �˾�")]
    [SerializeField] private ModalWindowManager startModalWindow;

    [Header("�÷��̾� �̵� ���� ������Ʈ")]
    [SerializeField] private PlayerMove playerMove;

    [Header("�� ������ ���� ������Ʈ")]
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
