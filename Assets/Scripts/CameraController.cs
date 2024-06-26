using UnityEngine;
using Mirror;

public class CameraController : NetworkBehaviour
{
    [SerializeField, Range(0,10)] private float _sensivity = 1;
    [SerializeField] private float _maxAngle = 90;

    private float _rotY;
    private float _rotX;
    private float _rotationY = 0;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            gameObject.SetActive(false);
            
        } 
        else
        {
            gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            _rotX = Input.GetAxis("Mouse X") * _sensivity;
            _rotY = -Input.GetAxis("Mouse Y") * _sensivity;

            _rotationY += _rotY;
            _rotationY = Mathf.Clamp(_rotationY, -_maxAngle, _maxAngle);
            transform.localRotation = Quaternion.Euler(_rotationY, 0, 0);

            transform.parent.rotation *= Quaternion.Euler(0, _rotX, 0);  
        }
    }
}
