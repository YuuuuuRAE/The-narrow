//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[DisallowMultipleComponent]
public class MainMenuScene : MonoBehaviour
{
    Data data;

    [Header("클리어 이미지 배열")]
    [SerializeField] private GameObject[] clearImage;

    private void Start()
    {
        Cursor.visible = true;

        data = DataManager.Instance.data;

        for (int i = 0; i < data.clear.Length; i++)
        {
            if (data.clear[i])
                clearImage[i].SetActive(true);
            else
                clearImage[i].SetActive(false);
        }
    }
}
