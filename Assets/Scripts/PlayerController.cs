using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0,10)] private float _speed = 1;
    [SerializeField, Range(0, 50)] private float _gravity = 9.81f;
    [SerializeField, Range(0,10)] private float _jumpHeight = 1;

    private CharacterController controller;
    private Vector3 _moveDirection = Vector3.zero;
    
    bool isFlip = false;
    

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        
    }

    private void Update()
    {
        //FALL
        if (!controller.isGrounded)
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }

        //MOVE
        float directionY = _moveDirection.y;
        _moveDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");


        //JUMP
        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            _moveDirection.y = _jumpHeight;
        }
        else
        {
            _moveDirection.y = directionY;
        }

        //APPLY MOVING
        controller.Move(_moveDirection * _speed * Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            FlipGravity();
        }
    }

    private void FlipGravity()
    {
        _gravity *= -1;

        if (isFlip)
        {
            transform.rotation = (Quaternion.Euler(0, 0, 0));
            isFlip = false;
        }
        else
        {
            transform.rotation = (Quaternion.Euler(0, 0, 180));
            isFlip = true;
        }

    }
}
