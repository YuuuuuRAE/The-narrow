//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
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

    private void Update()
    {
        //Call Camera Rotation Method in Update
        CameraRotation();

        //Call Player Rotation Method in Fixed Update
        PlayerRotation();
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
