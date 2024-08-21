using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using XEntity.InventoryItemSystem;

public class Quiz : MonoBehaviour
{
    public GameObject quizPanel;
    public ItemManager itemManager;

    public GameObject inputfieldPanel;

    void Update()
    {
        if(!quizPanel.activeSelf)
        {
            for(int i = 0; i < itemManager.quizNum.Length; i++)
            {
                if(itemManager.quizNum[i].activeSelf){
                    itemManager.quizNum[i].SetActive(false);
                }
            }
        }

    }



}
