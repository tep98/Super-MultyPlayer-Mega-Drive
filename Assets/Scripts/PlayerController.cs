using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SerializeField, Range(0,10)] private float _speed = 1;
    [SerializeField] private float _gravity = 9.81f;
    [SerializeField, Range(0,10)] private float _jumpHeight = 1;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundLayer;
    private bool _isGrounded = false;

    private CharacterController controller;
    private Vector3 _moveDirection = Vector3.zero;
    
    bool isFlip = false;
    

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        //FALL
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
        if (!_isGrounded)
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }

        //MOVE
        float directionY = _moveDirection.y;
        _moveDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");


        //JUMP
        if (Input.GetButton("Jump") && _isGrounded)
        {
            if (isFlip)
            {
                _moveDirection.y = -_jumpHeight;
            }
            else
            {
                _moveDirection.y = _jumpHeight;
            } 
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
        _moveDirection.y *= -1;


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
