//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMove : MonoBehaviour
{
    [Header("움직임 가능 관련 여부 Bool 변수")]
    public bool canMove = false;

    [Header("플레이어의 '걷기' 이동 속도")]
    [SerializeField] private float walkSpeed;

    //Real Apply Speed
    private float applySpeed;

    [Header("점프력")]
    [SerializeField] private float jumpPower;

    [Header("웅크렸을 때 속도")]
    [SerializeField] private float crouchSpeed;

    [Header("웅크렸을 때의 Y 위치")]
    [SerializeField] private float crouchPosY;

    //Player's Rigidbody
    private Rigidbody rigid;

    //Player's CapsuleCollider
    private CapsuleCollider capsuleCollider;

    //Camera
    [Header("카메라 오브젝트")]
    [SerializeField] private Camera cam;

    //Can Jump
    private bool canJump = true;

    //Can Crouch
    private bool isCrouch = false;

    //Origin Position Y
    private float originPosY;

    //Apply Crouch Position Y
    private float applyCrouchPosY;

    //Key State Variables
    private bool downSpace;

    // SFX
    //FootStep Sound
    private string footStepSound;

    //FooStep Sound Play Coroutine
    private Coroutine footStepSoundPalyCoroutine;

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

    private void FixedUpdate()
    {
        //Call Move Method in Fixed Update
        if (canMove && !GameManager.instance.isPause)
        {
            //Move
            Move();

            //Jump
            Jump();
        }
    }

    private void Update()
    {
        if (canMove && !GameManager.instance.isPause)
        {
            //Check Landing
            CheckLanding();

            //Try Jump
            TryJump();

            //Crouch
            TryCrouch();

            //Play Move Sound
            PlayMoveSound();
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

    //Play Move Sound Method
    private void PlayMoveSound()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (footStepSoundPalyCoroutine == null)
                footStepSoundPalyCoroutine = StartCoroutine(PlayMoveSoundCoroutine());
        }
        else if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
        {
            if (footStepSoundPalyCoroutine != null)
            {
                footStepSoundPalyCoroutine = null;
                StopAllCoroutines();
            }
        }
    }

    //Play Move Sound Coroutine
    IEnumerator PlayMoveSoundCoroutine()
    {
        int randomIndex = Random.Range(0, 10);

        footStepSound = SoundManager.instance.sfxs[randomIndex].name;

        SoundManager.instance.PlaySFX(footStepSound);

        yield return new WaitForSeconds(1f);

        StartCoroutine(PlayMoveSoundCoroutine());
    }

    //Try Jump Method
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
            downSpace = true;
    }

    //Jump Method
    private void Jump()
    {
        //Key : Space
        if (downSpace && canJump)
        {
            downSpace = false;

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
