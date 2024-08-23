using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Suitcase : MonoBehaviour
{

    [SerializeField] private Quiz quiz;

    [SerializeField] private PlayerMove playerMove;

    [SerializeField] private InputField inputField;

    public GameObject suitcase;

    public GameObject diaryObject;
    public GameObject quizObject;

    public string password;

    public bool openSuitcase;

    void Update()
    {
        if(quiz.inputfieldPanel.activeSelf && openSuitcase){
            inputField.gameObject.SetActive(true);

            inputField.ActivateInputField();

            playerMove.canMove = false;
        }
    }

    public void Excute()
    {

        if(inputField.text == password){
            
            DeactivePanel();

            diaryObject.SetActive(true);

            quizObject.SetActive(true);

            SoundManager.instance.PlaySFX("Place Item");

            suitcase.tag = "Untagged";

        }
        else
        {
            if (!SoundManager.instance.IsPlaying("Failure"))
                SoundManager.instance.PlaySFX("Failure");
        }
    }


    public void DeactivePanel()
    {
        openSuitcase = false;

        inputField.gameObject.SetActive(false);

        inputField.DeactivateInputField();

        quiz.inputfieldPanel.SetActive(false);
            
        inputField.text = "";

        playerMove.canMove = true;
    }


}
