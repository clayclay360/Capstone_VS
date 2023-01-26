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
    private const float MOVESPEED = 5f;
    public float rotatingSpeed;
    public Rigidbody body;

    [Header("Camera")]
    public Camera playerCamera;
    private Vector3 camOffset = new Vector3(0, 10, -3);
    private Vector3 camRotation = new Vector3(70, 0, 0);

    //Animator
    [Header("Animator")]
    public Animator animator;

    void Awake()
    {
        playerCamera.transform.position = gameObject.transform.position + camOffset;
        playerCamera.transform.eulerAngles = camRotation;
    }

    #region movement
    void FixedUpdate()
    {
        RaycastCheck();
        PlayerMovement();
    }

    //Collects input from the controller
    public void OnMove(InputValue value)
    {
        Vector2 moveVal = value.Get<Vector2>();
        moveVec = new Vector3(moveVal.x, 0, moveVal.y);

        //if (rightAnalogMagnitude < 0.1f)
        //{
        //    Vector2 rotateVal = value.Get<Vector2>();
        //    rotateVec = new Vector3(rotateVal.x, 0, rotateVal.y);
        //}
    }

    //Collects input from the controller
    public void OnLook(InputValue value)
    {
        Vector2 rotateVal = value.Get<Vector2>();
        rotateVec = new Vector3(rotateVal.x, 0f, rotateVal.y).normalized;
        rightAnalogMagnitude = rotateVec.magnitude;
    }

    /// <summary>
    /// Checks for objects in front of the player and slows them appropriately
    /// </summary>
    private void RaycastCheck()
    {
        float range = 5f;
        Vector3 direction = new Vector3(transform.forward.x, 0.0f, transform.forward.z).normalized;
        Ray slowSpeedRay = new Ray(transform.position + new Vector3(0f, 1.0f, 0f), direction * range);
        Debug.DrawRay(transform.position + new Vector3(0f, 1.0f, 0f), direction * range, Color.red);

        if (Physics.Raycast(slowSpeedRay, out RaycastHit hit, range))
        {
            float hitDist = hit.distance;
            if (hitDist < 0.75f)
            {
                movingSpeed = 0f;
            }
            else if (hitDist < 5)
            {
                movingSpeed = Mathf.Clamp(hitDist * hitDist, 0.0f, 5.0f);
            }
        }
        else
        {
            movingSpeed = MOVESPEED;
        }
    }


    public void PlayerMovement()
    {
        //Set the animator to be 0 if the player is still or to the movement speed
        //Movement speed slows when player is in front of objects
        float animatorSpeed = moveVec.magnitude == 0 ? 0 : movingSpeed;
        animator.SetFloat("BlendX", animatorSpeed);

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
                Quaternion newRotation = Quaternion.LookRotation(rotateVec, Vector3.up);
                newRotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotatingSpeed);// * Time.deltaTime);
                body.MoveRotation(newRotation);
                //float angle = Mathf.Atan2(rotateVec.x, rotateVec.z) * Mathf.Rad2Deg;
                //Debug.Log(angle);
                //transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }
        }

        //Camera
        playerCamera.transform.position = gameObject.transform.position + camOffset;
    }
    #endregion
}
