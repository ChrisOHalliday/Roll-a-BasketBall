using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField] Transform playerInputSpace = default;
    [SerializeField] LayerMask playerWalkingLayer = default;

    private PlayerStats playerStats;
    private Rigidbody playerRb;
    private Collider playerCollider;
    private PlayerInput playerInput;
    private  InputSystemActions inputActions;
    private bool isGrounded = true;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        playerInput = GetComponent<PlayerInput>();
        inputActions = new InputSystemActions();
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += Jump;

    }

    private void FixedUpdate()
    {
        MovePlayer();
        //CapPlayerVelocity();
        //Debug.Log(playerRb.linearVelocity.magnitude);
    }

    private void CapPlayerVelocity()
    {
        if (playerRb.linearVelocity.magnitude >= 6.0f)
        {
            playerRb.linearVelocity = Vector3.ClampMagnitude(playerRb.linearVelocity, 6.0f);
        }
    }

    private void MovePlayer()
    {
        Vector2 inputMovementVector = inputActions.Player.Move.ReadValue<Vector2>();
        //Debug.Log(inputMovementVector);
        Vector3 inputVelocity = playerInputSpace.TransformDirection(inputMovementVector.x, 0, inputMovementVector.y);
        playerRb.AddForce(inputVelocity * playerStats.GetPlayerSpeed() * Time.fixedDeltaTime,ForceMode.Force);

    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded)
            {
                playerRb.AddForce(Vector3.up * playerStats.GetPlayerJumpHeight(),ForceMode.Impulse);
                isGrounded = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Ground")
        //{
        //    isGrounded = true;

        //}
        if ((playerWalkingLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            Debug.Log("ground");
            isGrounded = true;
        }
    }

}
