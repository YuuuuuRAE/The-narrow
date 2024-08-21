using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{

    [SerializeField] private Quiz quiz;

    [SerializeField] private PlayerMove playerMove;

    [SerializeField] private InputField inputField;

    public GameObject box;

    public GameObject diaryObject;

    public GameObject quizObject;

    public string password;

    public bool openBox;

    void Update()
    {
        if(quiz.inputfieldPanel.activeSelf && openBox){
            inputField.gameObject.SetActive(true);

            inputField.ActivateInputField();

            playerMove.canMove = false;
        }
    }

    public void Excute(){
        
        if(inputField.text == password){

            DeactivePanel();

            diaryObject.SetActive(true);

            quizObject.SetActive(true);

            box.tag = "Untagged";

        }


    }

    public void DeactivePanel()
    {
        quiz.inputfieldPanel.SetActive(false);

        playerMove.canMove = true;

        openBox = false;

        inputField.gameObject.SetActive(false);

        inputField.DeactivateInputField();

        inputField.text = "";
    }

}
