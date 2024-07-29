//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMove : MonoBehaviour
{
    public bool canMove = false;

    [Header("플레이어의 '걷기' 이동 속도")]
    [SerializeField] private float walkSpeed;

    //Player's Rigidbody
    private Rigidbody rigid;

    private void Awake()
    {
        //Init
        canMove = false;

        //Get Component
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Call Move Method in Fixed Update
        if (canMove) Move();
    }

    //Move Method
    private void Move()
    {
        rigid.velocity = Vector3.zero;

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
}
