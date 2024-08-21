using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEntity.InventoryItemSystem;

public class Diary : MonoBehaviour
{
    public GameObject diary;
    public ItemManager itemManager;


    void Update()
    {
        if(!diary.activeSelf)
        {
            for(int i = 0; i < itemManager.diaryPage.Length; i++)
            {
                if(itemManager.diaryPage[i].activeSelf)
                {
                    Debug.Log(i);
                    itemManager.diaryPage[i].SetActive(false);
                }
            }
        }


    }
}
