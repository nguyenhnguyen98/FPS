using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerManager : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField]
    private Vector3 playerVelocity;

    public float playerHeight;
    public float crouchingHeight;

    public float airMultiplier = 0.4f;
    public float normalSpeed;
    public float sprintSpeed;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public Vector3 drag;

    private float crouchTimer = 0f;
    private float playerSpeed;

    private bool isGrounded;
    private bool sprinting;
    private bool crouching;
    private bool lerpCrouch;


    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        playerSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = _characterController.isGrounded;

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
            {
                _characterController.height = Mathf.Lerp(_characterController.height, crouchingHeight, p);
            } else
            {
                _characterController.height = Mathf.Lerp(_characterController.height, playerHeight, p);
            }

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    public void ProcessMove(Vector2 input, float delta)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        if (isGrounded)
        {
            _characterController.Move(transform.TransformDirection(moveDirection) * playerSpeed * delta);
        } else if (!isGrounded)
        {
            _characterController.Move(transform.TransformDirection(moveDirection) * playerSpeed * airMultiplier * delta);
        }

        playerVelocity.y += gravity * delta;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

//        if (isGrounded)
//        {
//            playerVelocity.x /= 1f + drag.x;
//            playerVelocity.y /= 1f + drag.y;
//            playerVelocity.z /= 1f + drag.z;
//        }

        _characterController.Move(playerVelocity * delta);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Sprinting()
    {
        sprinting = !sprinting;

        if (sprinting)
        {
            playerSpeed = sprintSpeed;
        } else
        {
            playerSpeed = normalSpeed;
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0f;
        lerpCrouch = true;
    }
}
