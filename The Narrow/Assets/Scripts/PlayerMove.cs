//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMove : MonoBehaviour
{
    [Header("플레이어의 '걷기' 이동 속도")]
    [SerializeField] private float walkSpeed;

    [Header("카메라 컴포넌트")]
    [SerializeField] private Camera _camera;

    [Header("마우스 민감도")]
    [SerializeField] private float sensitivity;

    [Header("카메라 최대 회전 값")]
    [SerializeField] private float limitCameraRotation;

    //Current Camera Rotation.X value
    private float currentCameraRotationX;

    //Player's Rigidbody
    private Rigidbody rigid;

    private void Awake()
    {
        //Get Component
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Call Move Method in Fixed Update
        Move();
    }

    private void Update()
    {
        //Call Camera Rotation Method in Update
        CameraRotation();

        //Call Player Rotation Method in Fixed Update
        PlayerRotation();
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

    private void CameraRotation()
    {
        //Get rotation.X value
        float rotationX = Input.GetAxisRaw("Mouse Y");

        //Update current camera roation.X value
        currentCameraRotationX -= rotationX * sensitivity;

        //Clamp Current Camera Rotation.X value
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -limitCameraRotation, limitCameraRotation);

        //Apply to Camera
        _camera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void PlayerRotation()
    {
        //Get rotation.Y value
        float rotationY = Input.GetAxisRaw("Mouse X");

        //Set Player Rotation.Y value
        Vector3 playerRotationY = new Vector3(0f, rotationY, 0f) * sensitivity;

        //Applay to rigid
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(playerRotationY));
    }


}
