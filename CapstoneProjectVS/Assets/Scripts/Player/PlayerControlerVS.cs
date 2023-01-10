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
    private Vector3 moveVec;
    private Vector3 rotateVec;
    private float rightAnalogMagnitude;

    public float speed = 5f;
    private Rigidbody body;
    public float rotationSpeed;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().mass = 0;
        print(GetComponent<Rigidbody>().mass);
    }

    public void OnMove(InputValue value)
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move.Normalize();

        body.velocity = move * speed;

        transform.eulerAngles = new Vector3(0, 180, 0);
    }

    public void OnLook(InputValue value)
    {
        Debug.Log(2);
    }
}
