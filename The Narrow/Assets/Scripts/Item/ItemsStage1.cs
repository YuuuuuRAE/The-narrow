using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEntity.InventoryItemSystem;

public class ItemsStage1 : MonoBehaviour
{
    public GameObject diary;

    public GameObject[] diaryPage;

    public GameObject quiz;

    public GameObject[] quizNum;

    public ItemManager itemManager;

    private void Start()
    {
        itemManager = FindObjectOfType<ItemManager>();

        if (itemManager == null) return;

        itemManager.diary = diary;

        itemManager.diaryPage = diaryPage;

        itemManager.quiz = quiz;

        itemManager.quizNum = quizNum;
    }
}
