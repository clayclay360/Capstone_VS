using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //variables for movement and cameras
    [Header("Movement")]
    private Vector3 moveVec;
    private Vector3 rotateVec;
    private float rightAnalogMagnitude;
    public float movingSpeed;
    private const float MOVESPEED = 7f;

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
        PlayerMovement();
    }

    //Collects input from the controller
    public void OnMove(InputValue value)
    {
        Vector2 moveVal = value.Get<Vector2>();
        moveVec = new Vector3(moveVal.x, 0, moveVal.y);
    }
    //Collects input from the controller
    public void OnLook(InputValue value)
    {
        Vector2 rotateVal = value.Get<Vector2>();
        rotateVec = new Vector3(rotateVal.x, 0f, rotateVal.y).normalized;
        rightAnalogMagnitude = rotateVec.magnitude;
    }

    public void PlayerMovement()
    {
        //Set the animator to be 0 if the player is still or to the movement speed
        //Movement speed slows when player is in front of objects
        float animatorSpeed = moveVec.magnitude <= 0.05f ? 0 : movingSpeed;
        animator.SetFloat("BlendX", animatorSpeed);

        #region Raycast Set Speed
        //Raycast checks for an object in front of the player and its distance
        //The closer an object is in front of the player the lower their speed
        float range = 7f;
        Vector3 direction = new Vector3(transform.forward.x, 0.0f, transform.forward.z).normalized;
        Ray slowSpeedRay = new Ray(transform.position + new Vector3(0f, 1.0f, 0f), direction * range);
        if (Physics.Raycast(slowSpeedRay, out RaycastHit hit, range))
        {

            //forDist gets the current distance between the point and the player.
            //newDist is the distance between them AFTER the player moves.
            //We calculate this before movement so that the player can still
            //move away from the object even if they're facing it.
            float rayDist = hit.distance;
            float forDist = Vector3.Distance(transform.position, hit.point);
            float newDist = Vector3.Distance(transform.position + MOVESPEED * Time.deltaTime * moveVec, hit.point);
            if (rayDist < 0.75f && newDist < forDist)
            {
                movingSpeed = 0f;
            }
            else if (rayDist < range)
            {
                movingSpeed = Mathf.Clamp(Mathf.Pow(rayDist, 3), 0.0f, MOVESPEED);
            }
        }
        else
        {
            movingSpeed = MOVESPEED;
        }
        #endregion

        //Movement
        if (Mathf.Abs(moveVec.x) > 0 || Mathf.Abs(moveVec.z) > 0)
        {
            if (moveVec.magnitude > 0.1f)
            { 
                transform.position += movingSpeed * Time.deltaTime * moveVec;
            }
        }

        //Rotation
        rotateVec = rightAnalogMagnitude < 0.05f ? moveVec : rotateVec;
        if (Mathf.Abs(rotateVec.x) > 0 || Mathf.Abs(rotateVec.z) > 0)
        {
            Vector3 rotateDirection = (Vector3.right * rotateVec.x) + (Vector3.forward * rotateVec.z);
            if (Mathf.Abs(rotateDirection.magnitude) > 0.05f)
            {
                float angle = Mathf.Atan2(rotateVec.x, rotateVec.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }
        }

        //Camera
        playerCamera.transform.position = gameObject.transform.position + camOffset;
    }
    #endregion
}
