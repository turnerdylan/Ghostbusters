using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //serializables
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private Camera currentCam;


    //references
    private Rigidbody rb;
    private Animator _anim;
    private int _horizontalHash = 0;
    private int _verticalHash = 0;

    //private vars
    //world direction the guy moves
    private Vector3 moveDirection = Vector3.zero;
    //this is the input from joystick
    private Vector3 inputMoveVector = Vector3.zero;

    private Vector3 inputLookVector = Vector3.zero;
    private float storedLookValue;
    private CheckIfGrounded isGrounded;

    private float currentJumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        isGrounded = FindObjectOfType<CheckIfGrounded>();
        _horizontalHash = Animator.StringToHash("HorizSpeed");
        _verticalHash = Animator.StringToHash("VertSpeed");
    }

    public void SetMoveVector(Vector2 direction)
    {
        inputMoveVector = direction;
    }

    public void SetLookVector(Vector2 direction)
    {
        inputLookVector = direction;
    }

    public void Jump()
    {
        if (isGrounded.GetIsGrounded())
        {
            print("jumping");
            rb.velocity = new Vector3(inputMoveVector.x, jumpForce, inputMoveVector.y);
        }
    }

    void Update()
    {
        SetLookDirection();
    }

    private void FixedUpdate()
    {
        SetMoveDirection();
    }


    private void SetMoveDirection()
    {
        moveDirection = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.forward) * inputMoveVector;
        moveDirection *= moveSpeed; //does nothing, the move speed is set by animator
        _anim.SetFloat("input", inputMoveVector.magnitude);

        _anim.SetFloat(_horizontalHash, moveDirection.x);
        _anim.SetFloat(_verticalHash, moveDirection.y);
        //rb.MovePosition(transform.position + moveDirection);
    }

    private void SetLookDirection()
    {
        if (inputLookVector.magnitude > 0.5f)
        {
            storedLookValue = Mathf.Atan2(inputLookVector.x, inputLookVector.y);
            transform.rotation = Quaternion.Euler(0, storedLookValue * Mathf.Rad2Deg, 0);
            //transform.rotation = Quaternion.Lerp(transform.rotation, storedLookValue * Mathf.Rad2Deg, 0);

        }
        else
        {
            transform.rotation = Quaternion.Euler(0, storedLookValue * Mathf.Rad2Deg, 0);
        }
    }
}
