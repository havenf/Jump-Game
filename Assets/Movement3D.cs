using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class Movement3D : MonoBehaviour
{
    private float speed = 2.2f;
    private float sensitivityHor = 3.0f;
    private float sensitivityVert = 1.0f;
    private float minimumVert = -45.0f;
    private float maximumVert = 45.0f;
    private float jumpForce = 3.0f;
    private float _rotationX = 0;
    private CharacterController _charController;
    private Camera _camera;
    private Vector3 _movement;
    private float _verticalVelocity = 0;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {


        if (_charController.isGrounded)
        {
            float deltaYaw = Input.GetAxis("Mouse X") * sensitivityHor;
            transform.Rotate(0, deltaYaw, 0);

            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            _camera.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);

            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;

            _movement = transform.TransformDirection(new Vector3(deltaX, 0, deltaZ));

            // if (Input.GetButtonDown("Jump"))
            // {
            //     _verticalVelocity = jumpForce * 1.618f;
            // }

            if (Input.GetKey(KeyCode.S))
            {
                _verticalVelocity = jumpForce * 1.618f;
            }

        }
        else
        {
            float deltaYaw = Input.GetAxis("Mouse X") * sensitivityHor;
            transform.Rotate(0, deltaYaw, 0);

            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            _camera.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
        }

        _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        _movement.y = _verticalVelocity;

        _charController.Move(_movement * Time.deltaTime);
    }


    private IEnumerator CountJump()
    {
        yield return new WaitForSeconds(1);
    }
}