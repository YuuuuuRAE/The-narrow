using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEntity.InventoryItemSystem;

public class KeypadDoor : MonoBehaviour
{

    [SerializeField] private Quiz quiz;

    [SerializeField] private PlayerMove playerMove;

    [SerializeField] private ActionController actionController;

    [SerializeField] private InputField inputField;

    public string password;

    public bool openKeypadDoor;

    void Update()
    {
        if(quiz.inputfieldPanel.activeSelf && openKeypadDoor){
            inputField.gameObject.SetActive(true);

            inputField.ActivateInputField();

            playerMove.canMove = false;
        }
    }

    public void Excute()
    {

        if(inputField.text.Equals(password)){

            DeactivePanel();

            playerMove.canMove = false;

            actionController.clearWindow.ModalWindowIn();

        }
    }


    public void DeactivePanel()
    {
        openKeypadDoor = false;

        inputField.gameObject.SetActive(false);

        inputField.DeactivateInputField();

        quiz.inputfieldPanel.SetActive(false);
            
        inputField.text = "";

        playerMove.canMove = true;
    }


}
