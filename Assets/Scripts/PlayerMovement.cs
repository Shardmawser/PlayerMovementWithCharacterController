using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Reference/s")]
    CharacterController controller; //Character controller
    [SerializeField] Transform feet; //Used for the ground checking

    [Header("Movement")]
    [SerializeField] float speed; //Speed of the character
    [SerializeField] float gravity; //How strong the force pushing down on the player is

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    [SerializeField] LayerMask whatIsGround; //Ground layer
    [SerializeField] float groundDist = 0.1f; //Radius of ground check
    bool isGrounded;

    [Header("Jumping")]
    [SerializeField] float jumpHeight;

    Vector3 playerVelocity; //Velocity of the player
    Vector3 move; //The direction the player should be moving to
    float x;
    float z;

	void Start()
	{
        controller = GetComponent<CharacterController>(); //Takes the character controller component
	}

	void Update()
    {
        isGrounded = Physics.CheckSphere(feet.position, groundDist, whatIsGround); //Ground check

        x = Input.GetAxis("Horizontal"); //Checks if A or D is pressed
        z = Input.GetAxis("Vertical"); //Checks if W or S is pressed

        move = transform.right * x + transform.forward * z; //Calculate player direction

        controller.Move(move.normalized * speed * Time.deltaTime); //Moves the player based on the move variable. "Move" is normalized to prevent the player from speeding up when moving diagonally

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = 0; //Reset y velocity if the player is grounded and the velocity is less than 0

        if(isGrounded && Input.GetKeyDown(jumpKey))
		{
            Jump();
		}

        playerVelocity.y += gravity * Time.deltaTime; //Set gravity 

        controller.Move(playerVelocity * Time.deltaTime); //Apply gravity
    }

    void Jump()
	{
        float jumpForce = Mathf.Sqrt(jumpHeight * -2 * gravity);

        playerVelocity.y = jumpForce;
	}
}
