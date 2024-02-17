using System.Collections;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    private GameObject _playerObject;
    private float speed = 1.0f;
    private float obstacleRange = 2.0f;

    private void Start()
    {
        _playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Use WASD for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(movement * speed * Time.deltaTime);

        // Rest of your code remains unchanged...

        Ray ray = new Ray(transform.position, transform.forward);
        Ray ray2 = new Ray(transform.position, transform.up);

        RaycastHit hit;
        RaycastHit hit2;

        if (Physics.Raycast(ray, out hit, obstacleRange) && hit.collider.gameObject != gameObject)
        {
            float angle = Random.Range(-110, 110);
            transform.Rotate(0, angle, 0);
        }

        if (Physics.Raycast(ray2, out hit2, obstacleRange) && hit2.collider.gameObject != _playerObject)
        {
            Debug.Log($"{hit2.collider.name} jumped over {this.name}");
            StartCoroutine(CountJump());
        }
    }

    private IEnumerator CountJump()
    {
        yield return new WaitForSeconds(1);
    }
}