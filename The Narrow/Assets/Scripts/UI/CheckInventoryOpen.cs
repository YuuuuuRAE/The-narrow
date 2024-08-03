//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[DisallowMultipleComponent]
public class CheckInventoryOpen : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove;
    [SerializeField] WallController WallController;
    [SerializeField] GameObject blackBackGround;

    private void OnEnable()
    {
        if (WallController.gameStart)
        {
            playerMove.canMove = false;

            blackBackGround.SetActive(true);

            //SoundManager.instance.PlaySFX("Hover");
        }

    }

    private void OnDisable()
    {
        if (WallController.gameStart)
        {
            playerMove.canMove = true;

            blackBackGround.SetActive(false);

            //SoundManager.instance.PlaySFX("Click");
        }

    }
}
