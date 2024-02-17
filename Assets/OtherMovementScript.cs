using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
// [AddComponentMenu("Control Script/FPS Input")]
public class OtherMovementScript : MonoBehaviour
{
    private GameObject _playerObject;
    private float obstacleRange = 2.0f;
    private float speed = 2.2f;
    private float sensitivityHor = 3.0f;
    private float sensitivityVert = 1.0f;
    private float minimumVert = -45.0f;
    private float maximumVert = 45.0f;
    private float jumpForce = 2.0f;
    private float _rotationX = 0;
    private CharacterController _charController;
    private Camera _camera;
    private Vector3 _movement;
    private float _verticalVelocity = 0;
    private int colliderCount = 0;
    private ScoringScript scoringScript;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerObject = GameObject.FindGameObjectWithTag("Player2");
        scoringScript = GameObject.FindGameObjectWithTag("Scoring").GetComponent<ScoringScript>();
    }

    void Update()
    {

        Ray ray = new Ray(transform.position, transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, obstacleRange) && hit.collider.gameObject != _playerObject)
        {
            if (hit.collider.name == "Player")
            {
                StartCoroutine(CountJump(hit.collider.name, this.name));
                scoringScript._playerOneScore += 100d;
            }
            if (hit.collider.tag == "Terrain")
            {
                do
                {
                    colliderCount += 1;
                } while (hit.collider.tag == "Terrain");

                do
                {
                    colliderCount -= 1;
                } while (hit.collider.tag != "Terrain");


                if (colliderCount == 1000)
                {
                    _charController.Move(new Vector3(0, 0, 20));
                    colliderCount = 0;
                }
            }
        }

        if (_charController.isGrounded)
        {
            // Use IJKL for movement
            float horizontalInput = 0;
            float verticalInput = 0;

            if (Input.GetKey(KeyCode.I))
            {
                verticalInput = 1;
            }
            else if (Input.GetKey(KeyCode.K))
            {
                _verticalVelocity = jumpForce * 1.618f;
            }

            if (Input.GetKey(KeyCode.J))
            {
                horizontalInput = -1;
            }
            else if (Input.GetKey(KeyCode.L))
            {
                horizontalInput = 1;
            }

            float deltaYaw = Input.GetAxis("Mouse X") * sensitivityHor;
            transform.Rotate(0, deltaYaw, 0);

            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            _camera.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);

            _movement = transform.TransformDirection(new Vector3(horizontalInput, 0, verticalInput));
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

        _charController.Move(_movement * speed * Time.deltaTime);
    }

    private IEnumerator CountJump(string colliderName, string playerName)
    {
        Debug.Log($"{colliderName} jumped over {playerName}");
        yield return new WaitForSeconds(1);
    }
}