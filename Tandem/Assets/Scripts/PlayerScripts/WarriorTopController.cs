﻿using UnityEngine;
using System;
public class WarriorTopController : PlayerTopScript {

    public GameObject shield;
    public GameObject WarriorTurningActive;
    public float shieldRotateSpeed = 5f;
    public Vector3 shieldOffset = new Vector3(0f, 1.0f, 1.0f);

    private GameObject activeShield;

    private float blockX;
    private float blockY;
    private float stickDir;

    private float aimRange = 90f;
    private float leftRange;
    private float rightRange;

    void FixedUpdate()
    {
        float turn = Input.GetAxis("Horizontal");
        if (turn != 0)
        {
            WarriorTurningActive.SetActive(true);
            Turn(turn);
        } else
        {
            WarriorTurningActive.SetActive(false);
        }

        //Axis input for aiming shield
        blockX = Input.GetAxis("BlockX");
        blockY = Input.GetAxis("BlockY");

        // Math to convert Axis to Angle Direction
        stickDir = Mathf.Atan2(blockX, blockY) * Mathf.Rad2Deg;

        if (Input.GetAxis("Block") > 0)
        {
            Block();
        }
        else if (!Input.GetButton("Block"))
        {
            StopBlock();
        }

    }

    /*
    * Blocking Code
    */

    /* Activate the player's block by creating a shield object to physically block projectiles */
    void Block()
    {
        // if shield does not exist
        if (activeShield == null)
        {
            // Init shield
            activeShield = Instantiate(shield);
        }
        // Shield exist, now aim the shield
        else {
            // Rotate shield in the direction of the Joystick
            LimitAimCone();
        }
    }

	void LimitAimCone()
	{
		float playerRotation = gameObject.transform.eulerAngles.y;
		float shieldRot = playerRotation + stickDir;

        activeShield.transform.rotation = Quaternion.Euler(new Vector3(0, shieldRot, 0));

		if (shieldRot > 360)
		{
			shieldRot = shieldRot - 360f;
		}
		if (shieldRot < 0)
		{
			shieldRot = shieldRot + 360f;
		}

		leftRange = playerRotation - aimRange;
		rightRange = playerRotation + aimRange;


		// Find Min Ranges
		if (leftRange < 0f)
		{
			leftRange = leftRange + 360f;
		}

		// Find Max Ranges
		if (rightRange > 360f)
		{
			rightRange = rightRange - 360f;
		}

		// EDGE CASE
		// if leftRange is larger than rightRange
		// the cone will be from min to 0 to max
		// this case need to be cover :(
		if (leftRange > rightRange)
		{
			if (shieldRot < leftRange && shieldRot > leftRange - aimRange)
			{
				UpdatePosition(leftRange);
				LimitStickDir(leftRange);
			}
			else if (shieldRot > rightRange && shieldRot < rightRange + aimRange)
			{
				UpdatePosition(rightRange);
				LimitStickDir(rightRange);
			}
			else {
				UpdatePosition(shieldRot);
			}
		}
		// NORMAL CASE HERE
		else {
			// Update Shield with Range Restrictions
			// Maintain Min Value if Rotation is under Min
			if (shieldRot < leftRange)
			{
				UpdatePosition(leftRange);
				LimitStickDir(leftRange);
			}
			// Maintain Max Value if Rotation is under Max
			else if (shieldRot > rightRange)
			{
				UpdatePosition(rightRange);
				LimitStickDir(rightRange);
			}
			// All good, No limit
			else {
				UpdatePosition(shieldRot);
			}
		}
	}

    void LimitStickDir(float angle)
    {
        // Controller Direction angles are 180 to -180
        // Convert rotation angle to controller Angle
        if (angle > 180)
        {
            angle = angle - 360;
        }
        activeShield.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

	void UpdatePosition(float angle)
	{
		// Shield Should Rotate Around Player
		float rads = angle * Mathf.Deg2Rad;

		// Math here...
		// Rotation around Origin with Vector [0, 0, 0.5]
		float newX = Mathf.Sin(rads) * 1;
		float newZ = Mathf.Cos(rads) * 1;
		// Make new vector for the shield position
		Vector3 newPos = new Vector3(newX, 0, newZ);

		Physics.IgnoreLayerCollision(9, 10, true);
		// Physics.Ignore;
		// Move the shield into place.  Have to offset the shield from the player
		activeShield.transform.position = 
			newPos + 
			GameObject.Find("CompletePlayer").transform.position +
			Vector3.up * transform.GetComponent<CapsuleCollider>().height / 2;

	}

    /* Cease blocking by destroying the shield */
    void StopBlock()
    {
        Destroy(activeShield);
    }

    void OnDisable()
    {
        if (activeShield) Destroy(activeShield);
    }
}
