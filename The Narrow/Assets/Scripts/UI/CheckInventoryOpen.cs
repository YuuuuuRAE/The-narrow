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

    private void OnEnable()
    {
        if (WallController.gameStart)
        {
            playerMove.canMove = false;

            SoundManager.instance.PlaySFX("Hover");
        }

    }

    private void OnDisable()
    {
        if (WallController.gameStart)
        {
            playerMove.canMove = true;

            SoundManager.instance.PlaySFX("Click");
        }

    }
}
