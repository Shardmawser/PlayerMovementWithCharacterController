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

    [Header("Ground Check")]
    [SerializeField] LayerMask whatIsGround; //Ground layer
    [SerializeField] float groundDist = 0.1f; //Radius of ground check
    bool isGrounded;

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

        x = Input.GetAxisRaw("Horizontal"); //Checks if A or D is pressed
        z = Input.GetAxisRaw("Vertical"); //Checks if W or S is pressed

        move = transform.right * x + transform.forward * z; //Calculate player direction

        controller.Move(move.normalized * speed * Time.deltaTime); //Moves the player based on the move variable. "Move" is normalized to prevent the player from speeding up when moving diagonally

        if (isGrounded && playerVelocity.y > 0)
            playerVelocity.y = 0; //Reset y velocity if the player is grounded

        playerVelocity.y += gravity; //Set gravity 

        controller.Move(playerVelocity); //Apply gravity
    }
}
