using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("References")]
    PlayerMovement movement;

    [Header("Detection")]
    [SerializeField] float wallDist;
    bool wallLeft;
    bool wallRight;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();  
    }

    void Update()
    {
        CheckWall();

        if(CanWallRun())
		{
			if (wallLeft)
			{
                StartWallRun();
			}
            else if (wallRight)
			{
                StartWallRun();
			}
			else
			{
                StopWallRun();
			}
		}
        else
        {
            StopWallRun();
        }
    }

    bool CanWallRun()
	{
        return !movement.isGrounded;
	}

    void CheckWall()
	{
        wallLeft = Physics.Raycast(transform.position, -transform.right, wallDist);
        wallRight = Physics.Raycast(transform.position, transform.right, wallDist);
	}

    void StartWallRun()
	{
        movement.useGravity = false; 
	}
    void StopWallRun()
	{
        movement.useGravity = true; 
	}
	
}
