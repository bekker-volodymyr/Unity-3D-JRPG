using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private float playerVelocityY;
    private float gravityValue = -9.80f;
    private float jumpHeight = 0.1f;
    private bool groundedPlayer;

    private CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (groundedPlayer && playerVelocityY < 0)
        {
            playerVelocityY = 0f;
        }

        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        if (Mathf.Abs(dx) > 0 && Mathf.Abs(dy) > 0)
        {
            dx *= 0.707f;
            dy *= 0.707f;
        }

        if (Input.GetButton("Jump") && groundedPlayer)
        {
            playerVelocityY += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        Vector3 horizontalForward = Camera.main.transform.forward;
        horizontalForward.y = 0f;
        horizontalForward = horizontalForward.normalized;

        playerVelocityY += gravityValue * Time.deltaTime;
        _characterController.Move(
            (speed * (dx * Camera.main.transform.right + dy * horizontalForward) + playerVelocityY * Vector3.up) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            groundedPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            groundedPlayer = false;
        }
    }
}
