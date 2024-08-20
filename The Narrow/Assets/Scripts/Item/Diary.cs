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
        if(Input.GetKeyDown(KeyCode.E)){
            for(int i = 0; i < 5; i++)
            if(itemManager.diaryPage[i].activeSelf)
                itemManager.diaryPage[i].SetActive(false);

            diary.SetActive(false);
        }


    }
}
