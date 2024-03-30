using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class GravityScaler : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _gravity;
    bool isFlip = false;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _gravity = Physics.gravity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            FlipGravity();
        }
    }

    private void FixedUpdate()
    {
        _rb.AddForce( _gravity, ForceMode.Acceleration );
    }

    private void FlipGravity()
    {
        _gravity *= -1;

        transform.Rotate(0,0,180,Space.Self);
    }
}
