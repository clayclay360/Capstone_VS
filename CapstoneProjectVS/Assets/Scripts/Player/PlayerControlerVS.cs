using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using UnityEngine.AI;

public class PlayerControlerVS : MonoBehaviour
{
    //variables for movement and cameras
    [Header("Movement")]
    private Vector3 moveVec;
    private Vector3 rotateVec;
    private float rightAnalogMagnitude;
    public float movingSpeed;
    public float rotatingSpeed;
    public float movingSpeedTemp;

    [Header("Camera")]
    public Camera playerCamera;
    private Vector3 camOffset = new Vector3(0, 10, -3);
    private Vector3 camRotation = new Vector3(70, 0, 0);

    //variables for raycast
    public float range = 5;

    void Awake()
    {
        movingSpeedTemp = movingSpeed;
        playerCamera.transform.position = gameObject.transform.position + camOffset;
        playerCamera.transform.eulerAngles = camRotation;
    }

    void Update()
    {
        Vector3 direction = Vector3.forward;
        Ray slowSpeedRay = new Ray(transform.position + new Vector3(0f, 1.0f, 0f), transform.TransformDirection(direction * range));
        Debug.DrawRay(transform.position + new Vector3(0f, 1.0f, 0f), transform.TransformDirection(direction * range));

        if (Physics.Raycast(slowSpeedRay, out RaycastHit hit, range))
        {
            if (hit.collider.tag == "Obstacle")
            {
                //Debug.Log("Hitting Object");
                movingSpeed = 0;
            } else
            {
                movingSpeed = movingSpeedTemp;
            }
        }
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    //movement for the player
    public void OnMove(InputValue value)
    {
        Vector2 moveVal = value.Get<Vector2>();
        moveVec = new Vector3(moveVal.x, 0, moveVal.y);

        if (rightAnalogMagnitude < 0.1f)
        {
            Vector2 rotateVal = value.Get<Vector2>();
            rotateVec = new Vector3(rotateVal.x, 0, rotateVal.y);
        }
    }

    public void OnLook(InputValue value)
    {
        Vector2 rotateVal = value.Get<Vector2>();
        rotateVec = new Vector3(rotateVal.x, 0, rotateVal.y);
        rightAnalogMagnitude = rotateVec.magnitude;
    }

    public void PlayerMovement()
    {
        //Movement
        if (Mathf.Abs(moveVec.x) > 0 || Mathf.Abs(moveVec.z) > 0)
        {
            if (moveVec.magnitude > .1f)
            {
                transform.position += movingSpeed * Time.deltaTime * moveVec;
            }
        }
        //Rotation
        if (Mathf.Abs(rotateVec.x) > 0 || Mathf.Abs(rotateVec.z) > 0)
        {
            Vector3 rotateDirection = (Vector3.right * rotateVec.x) + (Vector3.forward * rotateVec.z);
            if (rotateDirection.sqrMagnitude > .1f)
            {
                Quaternion newRotation = Quaternion.LookRotation(rotateDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotatingSpeed);
                float angle = Mathf.Atan2(rotateVec.x, rotateVec.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }
        }

        //Camera
        playerCamera.transform.position = gameObject.transform.position + camOffset;
    }
}
