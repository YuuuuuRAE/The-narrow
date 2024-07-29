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

    //Related Walk
    [Header("플레이어의 '걷기' 이동 속도")]
    [SerializeField] private float walkSpeed;

    //Player's Rigidbody
    private Rigidbody rigid;

    //Can Jump
    [SerializeField] private bool canJump = true;

    [Header("점프력")]
    [SerializeField] private float jumpPower;

    private CapsuleCollider capsuleCollider;

    private void Awake()
    {
        //Init
        canMove = false;

        //Get Component
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        Debug.Log(rigid.velocity);

        //Call Move Method in Fixed Update
        if (canMove)
        {
            //Move
            Move();


        }
    }

    private void Update()
    {
        //Jump
        CheckLanding();
        Jump();
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
        Vector3 velocity  = (horizontal + vertical).normalized * walkSpeed;

        //Rigid : Move Position
        rigid.MovePosition(transform.position + velocity);
    }

    //Jump Method
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rigid.velocity = transform.up * jumpPower;
        }
    }

    //Check Landing Method
    private void CheckLanding()
    {
        canJump = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }
}
