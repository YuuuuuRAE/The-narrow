//System
using System.Collections;
using System.Collections.Generic;
using TMPro;

//Unity
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class ActionController : MonoBehaviour
{
    [Header("습득 가능 최대 거리")]
    [SerializeField] private float range;

    private bool canPickUp;

    private RaycastHit hit;

    [Header("아이템 레이어 마스크 정보")]
    [SerializeField] private LayerMask layerMask;

    [Header("Action Text 오브젝트")]
    [SerializeField] private TextMeshProUGUI actionText;

    private void Update()
    {
        //Call Check Item Method
        CheckItem();

        //Call Try Action Method
        TryAction();
    }


    //Tray Action Method : Key is 'E'
    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Check Item
            CheckItem();

            //Pick Up
            PickUp();
        }
    }

    //PickUp Method
    private void PickUp()
    {
        //Only Can Pick Up When canPickUp is true
        if (canPickUp)
        {
            if (hit.transform != null)
            {
                //Debug.Log
                Debug.Log("획득");
                Destroy(hit.transform.gameObject);
                DisappearItemInfo();
            }
        }
    }

    //Check Item Method
    private void CheckItem()
    {
        //Debug Draw Ray
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.red);

        //Check Ray Cast
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range, layerMask))
        {
            //Compare Tag
            if (hit.transform.CompareTag("Item"))
            {
                //Call Appear Item Info Method
                AppearItemInfo();
            }
        }
        //Call Disappear Item Info Method
        else DisappearItemInfo();
    }

    //Appear Item Info Method
    private void AppearItemInfo()
    {
        //Player Can Pick Up Item
        canPickUp = true;

        //Set Active : True
        actionText.gameObject.SetActive(true);

        //Update Text
        actionText.text = hit.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 " + "(E)";
    }

    //Disappear Item Info Method
    private void DisappearItemInfo()
    {
        //Player Can't Pick Up Item
        canPickUp = false;

        //Set Active : false
        actionText.gameObject.SetActive(false);
    }
}
