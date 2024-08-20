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

    public PlayerMove playerMove;


    public GameObject inputfieldPanel;

    public InputField[] field;

    public int password;

    public int inputNum;

    public Text feedbackText;

    public GameObject[] quiz;

    public GameObject[] diary;

    public GameObject suitcase;

    void Awake()
    {
        for(int i = 0; i < field.Length; i++){
            field[i].text = "";
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            for(int i = 0; i < 3; i++)
                if(itemManager.quizNum[i].activeSelf)
                    itemManager.quizNum[i].SetActive(false);

            quizPanel.SetActive(false);
        }

        if(inputfieldPanel.activeSelf){
            Debug.Log("1");
            field[inputNum].gameObject.SetActive(true);
            field[inputNum].ActivateInputField();
            
            if(Input.GetKeyDown(KeyCode.Return)){
                inputfieldPanel.SetActive(false);
                field[inputNum].gameObject.SetActive(false);
                field[inputNum].DeactivateInputField();
                quizPanel.SetActive(false);
                playerMove.canMove = true;
            }

            OnPasswordEntered(field[inputNum].text);
        }

    }


    public void OnPasswordEntered(string userInput)
    {
        int enteredPassword;
        if (int.TryParse(userInput, out enteredPassword) && userInput != null)
        {
            if (enteredPassword == password)
            {
                Debug.Log("2");
                feedbackText.text = "비밀번호가 맞습니다!";
                inputfieldPanel.SetActive(false);
                playerMove.canMove = true;
                field[inputNum].DeactivateInputField();
                
                quiz[inputNum].SetActive(true);
                diary[inputNum].SetActive(true);

                suitcase.tag = "Untagged";

            }
            else
            {
                Debug.Log("2");
                feedbackText.text = "비밀번호가 틀렸습니다. 다시 시도하세요.";
                field[inputNum].ActivateInputField();
            }
        }
        else
        {
            feedbackText.text = "숫자를 입력해주세요.";
            field[inputNum].ActivateInputField();
        }
    }



}
