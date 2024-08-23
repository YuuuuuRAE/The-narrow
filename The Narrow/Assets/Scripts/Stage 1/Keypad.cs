using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    
    [SerializeField] private Text text;

    [SerializeField] private string password;

    [SerializeField] private PlayerMove playerMove;

    public GameObject keypadPanel;

    public GameObject bookshelf;

    public GameObject quiz;
    public GameObject diary;

    public void Number(int number){
        if(text.text.Length >= 4)
            return;
        if(text.text == "키패드 입력"){
            text.text = "";
        }
        text.text += number.ToString();
        SoundManager.instance.PlaySFX("Keypad Click");
    }

    public void Excute(){
        if(text.text == password){
            
            DeactivePanel();

            bookshelf.tag = "Untagged";

            quiz.SetActive(true);
            diary.SetActive(true);

            SoundManager.instance.PlaySFX("Place Item");
        }
        else
        {
            if (!SoundManager.instance.IsPlaying("Failure"))
                SoundManager.instance.PlaySFX("Failure");
        }

        text.text = "";
    }

    public void DeactivePanel(){
        playerMove.canMove = true;

        keypadPanel.SetActive(false);

        text.text = "";
    }

}
