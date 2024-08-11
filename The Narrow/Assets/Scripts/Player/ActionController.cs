//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

//TMPro
using TMPro;

//Dark UI
using Michsky.UI.Dark;

//Name Space : XEntity Inventory System
namespace XEntity.InventoryItemSystem
{
    [DisallowMultipleComponent]
    public class ActionController : MonoBehaviour
    {
        [Header("습득 가능 최대 거리")]
        [SerializeField] private float range;

        //State Variable : Can Pick Up (Pick Up Item)
        private bool canPickUp;

        //State Variable : Can Open (Door, Chest)
        private bool canOpen;

        private bool canOpenVent;

        //Variable of RayCastHit
        private RaycastHit hit;

        [Header("레이어 마스크 정보")]
        [SerializeField] private LayerMask objLayerMask;

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

        [Header("벤트를 열 수 없음을 알려주는 팝업")] //*
        [SerializeField] private ModalWindowManager cantOpenVent;

        [Header("벤트를 열었음을 알려주는 팝업")] //*
        [SerializeField] private ModalWindowManager openVent;

        [SerializeField] private PlayerMove playerMove;

        private void Awake()
        {
            playerMove = transform.parent.GetComponent<PlayerMove>();
        }

        private void Update()
        {
            if (playerMove.canMove)
            {
                //Call Check Item Method
                CheckItem();

                //Call Try Action Method
                TryAction();
            }
        }

        #region 레이를 통한 아이템 체크 및 상태 변수 변경

        /// <summary>
        /// Initialize State Variabe of All
        /// </summary>
        private void Init()
        {
            canPickUp = false;
            canOpen = false;
            canOpenVent = false; //* Initialize Vent State
        }

        /// <summary>
        /// Checking Item
        /// </summary>
        private void CheckItem()
        {
            //Debug Draw Ray
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.red);

            //Check Ray Cast
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range, objLayerMask))
            {
                //Compare Tag
                if (hit.transform.CompareTag("Item"))
                {
                    //Call Pick Up Item Info Method
                    AppearPickUpInfo();
                }
                else if (hit.transform.CompareTag("Door"))
                {
                    //Call Appear Door Info Method
                    AppearDoorInfo();
                }
                else if (hit.transform.CompareTag("Book"))
                {
                    //Call Appear Book Info Method
                    AppearBookInfo();
                }
                else if (hit.transform.CompareTag("Vent"))
                {
                    //*Call Appear Vent Info Method
                    AppearVentInfo();
                }
                else if (hit.transform.CompareTag("BookShelf"))
                {
                    //*Call Appear BookShelf Info Method
                    AppearBookShelfInfo();
                }
                else if (hit.transform.CompareTag("Table"))
                {
                    //*Call Appear Table Info Method
                    AppearTableInfo();
                }
                
            }
            //Call Disappear Information Method
            else DisappearInfo();
        }

        /// <summary>
        /// Appear Pick Up Item Information
        /// </summary>
        private void AppearPickUpInfo()
        {
            //Player Can Pick Up Item
            canPickUp = true;

            //Set Active : True
            actionText.gameObject.SetActive(true);

            //Update Text
            actionText.text = hit.transform.GetComponent<InstantHarvest>().harvestItem.itemName + " 획득 " + "(E)";
        }

        /// <summary>
        /// Appear Door Information
        /// </summary>
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

        /// <summary>
        /// Appear Book Information 
        /// </summary>
        private void AppearBookInfo()
        {
            actionText.gameObject.SetActive(true);

            actionText.text = "책 보기 " + "(E)";
        }

        ///* <summary>
        /// Appear Vent Information 
        /// </summary>
        private void AppearVentInfo()
        {
            actionText.gameObject.SetActive(true);

            actionText.text = "환풍구 열기 " + "(E)";
            
            //Check Key
            foreach (ItemSlot item in itemContainer.slots)
            {
                //Pass Loop
                if (item.slotItem == null) continue;

                //Check Key
                if (item.slotItem.itemName == "Key") canOpenVent = true;
                else canOpenVent = false;
            }
        }

        ///* <summary>
        /// Appear BookShelf Information 
        /// </summary>
        private void AppearBookShelfInfo()
        {
            actionText.gameObject.SetActive(true);

            actionText.text = "책장 확인 " + "(E)";
            
        }


        ///* <summary>
        /// Appear Table Information 
        /// </summary>
        private void AppearTableInfo()
        {
            actionText.gameObject.SetActive(true);

            actionText.text = "책상 확인 " + "(E)";
            
        }

        /// <summary>
        /// Disappear Information Method
        /// </summary>
        private void DisappearInfo()
        {
            //Initialize
            Init();

            //Set Active : false
            actionText.gameObject.SetActive(false);
        }

        #endregion

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

                //*Open Vent
                OpenVent();
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

                    DisappearInfo();
                }
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

                    playerMove.canMove = false;
                }
                else if (!canOpen)
                {
                    cantOpenWindow.ModalWindowIn();

                    SoundManager.instance.PlaySFX("Unlock");

                    playerMove.canMove = false;
                }
            }
        }

        //*Open Vent Method
        private void OpenVent()
        {
            if (hit.transform == null) return;

            if (hit.transform.CompareTag("Vent"))
            {
                if (canOpenVent)
                {
                    openVent.ModalWindowIn();

                    playerMove.canMove = false;
                }
                else if (!canOpenVent)
                {
                    cantOpenVent.ModalWindowIn();

                    SoundManager.instance.PlaySFX("Unlock");

                    playerMove.canMove = false;
                }
            }
        }
    }
}


