//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

//TMPro
using TMPro;
using Michsky.UI.Dark;

namespace XEntity.InventoryItemSystem
{
    [DisallowMultipleComponent]
    public class ActionController : MonoBehaviour
    {
        [Header("습득 가능 최대 거리")]
        [SerializeField] private float range;

        private bool canPickUp;

        private bool canOpen;

        private RaycastHit hit;

        [Header("아이템 레이어 마스크 정보")]
        [SerializeField] private LayerMask itemLayerMask;

        [Header("문 레이어 마스크 정보")]
        [SerializeField] private LayerMask doorLayerMask;

        [Header("Action Text 오브젝트")]
        [SerializeField] private TextMeshProUGUI actionText;

        [Header("Interactor")]
        [SerializeField] private Interactor interactor;

        [Header("Item Container")]
        [SerializeField] private ItemContainer itemContainer;

        [Header("게임 클리어 팝업")]
        [SerializeField] private ModalWindowManager clearWindow;

        [Header("문을 열고 나갈 수 없음을 알려주는 팝업")]
        [SerializeField] private ModalWindowManager cantOpenWindow;

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

                //Open Door
                OpenDoor();
            }
        }

        //PickUp Method
        private void PickUp()
        {
            //Only Can Pick Up When canPickUp is true
            if (canPickUp)
            {
                if (hit.transform.CompareTag("Item"))
                {
                    //Debug.Log
                    Debug.Log("획득");

                    SoundManager.instance.PlaySFX("Item Pick Up");

                    hit.transform.GetComponent<InstantHarvest>().AttemptHarvest(interactor);

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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range, itemLayerMask))
            {
                //Compare Tag
                if (hit.transform.CompareTag("Item"))
                {
                    //Call Appear Item Info Method
                    AppearItemInfo();
                }
                else if (hit.transform.CompareTag("Door"))
                {
                    //Call Appear Door Info Method
                    AppearDoorInfo();
                }
            }
            //Call Disappear Item Info Method
            else DisappearItemInfo();
        }

        //Appear Door Info
        private void AppearDoorInfo()
        {
            actionText.gameObject.SetActive(true);

            actionText.text = "문 열기 " + "(E)";

            //Check Key
            foreach (ItemSlot item in itemContainer.slots)
            {
                //Pass Loop
                if (item.slotItem == null) continue;

                //Check Key
                if (item.slotItem.itemName == "Key") canOpen = true;
                else canOpen = false;
            }
        }

        //Open Door Method
        private void OpenDoor()
        {
            if (hit.transform == null) return;

            if (hit.transform.CompareTag("Door"))
            {
                if (canOpen)
                {
                    clearWindow.ModalWindowIn();

                    GameManager.instance.Pause();
                }
                else if (!canOpen)
                {
                    cantOpenWindow.ModalWindowIn();

                    GameManager.instance.Pause();
                }
            }

        }

        //Appear Item Info Method
        private void AppearItemInfo()
        {
            //Player Can Pick Up Item
            canPickUp = true;

            //Set Active : True
            actionText.gameObject.SetActive(true);

            //Update Text
            actionText.text = hit.transform.GetComponent<InstantHarvest>().harvestItem.itemName + " 획득 " + "(E)";
        }

        //Disappear Item Info Method
        private void DisappearItemInfo()
        {
            //Player Can't Pick Up Item
            canPickUp = false;

            //Player Can't Open the door
            canOpen = false;

            //Set Active : false
            actionText.gameObject.SetActive(false);
        }
    }
}


