using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    //private float moveFB, moveLR;

    //public float moveSpeed = 6f;
    //public float jumpSpeed = 8.0f;
    //public float gravity = 20.0f;

    //private Vector3 moveDirection = Vector3.zero;

    //[SerializeField] private MMOCharacterCam mMOCharacterCam;
    //private CharacterController characterController;
    //private float verticalVel;
    //private Vector3 moveVector;

    //private bool isGround;

    //private void Start()
    //{
    //    characterController = GetComponent<CharacterController>();
    //}
    //private void Update()
    //{
    //    if (characterController.isGrounded)
    //    {
    //        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
    //        moveDirection = transform.TransformDirection(moveDirection);
    //        moveDirection *= moveSpeed;

    //        if (Input.GetButton("Jump"))
    //        {
    //            moveDirection.y = jumpSpeed;
    //        }
    //    }

    //    moveDirection.y -= gravity * Time.deltaTime;

    //    // Move the controller
    //    characterController.Move(moveDirection * Time.deltaTime);

    //    if (Input.GetAxis("Vertical") != 0)
    //    {
    //        Quaternion turnAngle = Quaternion.Euler(0, mMOCharacterCam.target.eulerAngles.y, 0);

    //        transform.rotation = Quaternion.Slerp(transform.rotation, turnAngle, Time.deltaTime * mMOCharacterCam.rotationSpeed);
    //    }
    //}

    //private void PlayerGround()
    //{
    //    isGround = characterController.isGrounded;

    //    if (isGround)
    //    {
    //        verticalVel -= 0;
    //    }
    //    else
    //    {
    //        verticalVel -= 2;
    //    }
    //    moveVector = new Vector3(0, verticalVel, 0);
    //    characterController.Move(moveVector);
    //}

    //private void PlayerMoveAndRotation()
    //{
    //    moveFB = Input.GetAxis("Vertical");
    //    moveLR = Input.GetAxis("Horizontal");

    //    Vector3 move = new Vector3(moveLR, 0f, moveFB).normalized * moveSpeed * Time.deltaTime;
    //    //=================
    //    move = transform.rotation * move;

    //    characterController.Move(move);

    //    if (Input.GetAxis("Vertical") != 0)
    //    {
    //        Quaternion turnAngle = Quaternion.Euler(0, mMOCharacterCam.target.eulerAngles.y, 0);

    //        transform.rotation = Quaternion.Slerp(transform.rotation, turnAngle, Time.deltaTime * mMOCharacterCam.rotationSpeed);
    //    }
    //}
}
