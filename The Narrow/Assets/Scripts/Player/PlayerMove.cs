//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMove : MonoBehaviour
{
    [Header("������ ���� ���� ���� Bool ����")]
    public bool canMove = false;

    [Header("�÷��̾��� '�ȱ�' �̵� �ӵ�")]
    [SerializeField] private float walkSpeed;

    //Real Apply Speed
    private float applySpeed;

    [Header("������")]
    [SerializeField] private float jumpPower;

    [Header("��ũ���� �� �ӵ�")]
    [SerializeField] private float crouchSpeed;

    //Player's Rigidbody
    private Rigidbody rigid;

    //Player's CapsuleCollider
    private CapsuleCollider capsuleCollider;

    //Camera
    [Header("ī�޶� ������Ʈ")]
    [SerializeField] private Camera cam;

    //Can Jump
    private bool canJump = true;

    //Can Crouch
    private bool isCrouch = false;

    //Origin Position Y
    private float originPosY;

    //Crouch Position Y
    [Header("��ũ���� ���� Y ��ġ")]
    [SerializeField] private float crouchPosY;

    private float applyCrouchPosY;

    private void Awake()
    { 
        //Init
        canMove = false;
        originPosY = cam.transform.localPosition.y;
        applyCrouchPosY = originPosY;

        applySpeed = walkSpeed;

        //Get Component
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        //Call Move Method in Fixed Update
        if (canMove)
        {
            //Move
            Move();

            //Jump
            CheckLanding();
            Jump();

            //Crouch
            TryCrouch();
        }
    }

    //Move Method
    private void Move()
    {
        //Input Direction : Right and Left
        float dirX = Input.GetAxisRaw("Horizontal");

        //Input Direction : Forward and Back
        float dirZ = Input.GetAxisRaw("Vertical");

        //Set Vector3 : For compute velocity
        Vector3 horizontal = transform.right * dirX;
        Vector3 vertical = transform.forward * dirZ;

        //Set Velocity
        Vector3 velocity  = (horizontal + vertical).normalized * applySpeed;

        //Rigid : Move Position
        rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    //Jump Method
    private void Jump()
    {
        //Key : Space
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            //if player is crouching...
            if (isCrouch)
                Crouch();

            //Up!
            rigid.velocity = transform.up * jumpPower;
        }
    }

    //Check Landing Method
    private void CheckLanding()
    {
        canJump = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }

    //Try Crouch Method
    private void TryCrouch()
    {
        //Key : Shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Crouch();
        }
    }

    //Crouch Method
    private void Crouch()
    {
        //Conver Crouch State
        isCrouch = !isCrouch;

        if (isCrouch)
        {
            //Change Speed
            applySpeed = crouchSpeed;

            //Change Apply Crouch Position Y
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            //Change Speed
            applySpeed = walkSpeed;

            //Change Apply Crouch Position Y
            applyCrouchPosY = originPosY;
        }

        //Start Crouch Coroutine
        StartCoroutine(CrouchCoroutine());
    }

    //Crouch Coroutine
    IEnumerator CrouchCoroutine()
    { 
        //Camera's Position 'Y'
        float posY = cam.transform.localPosition.y;

        //Checking Variable
        int count = 0;

        while (posY != applyCrouchPosY)
        {
            count++;

            //Lerp Method
            posY = Mathf.Lerp(posY, applyCrouchPosY, 0.3f);

            //Apply to Camera transform
            cam.transform.localPosition = new Vector3(0, posY, 0);

            if (count > 15)
                break;
            //Skip 1 Frame
            yield return null;
        }

        //ReApply to Camera transform
        cam.transform.localPosition = new Vector3(0f, applyCrouchPosY, 0f);
    }
}
