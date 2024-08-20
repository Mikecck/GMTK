using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepControl : MonoBehaviour
{
	public float walkSpeed;
	public bool twoWalkStage;
	public Vector2 walkDirection1;
	public Vector2 walkDirection2;
	public int stageOneTime;
	public bool isIdle;
	public float idleSpeed;
	public int idleThreshold;

	Rigidbody2D rb;
	int countForward;
	int countBackward;
	int countStageOne;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (isIdle) Idle();
		else
		{
			if (!twoWalkStage) Walk();
			else
			{
				if (countStageOne < stageOneTime)
				{
					countStageOne++;
					WalkStageOne();
				}
				else Walk();
			}
		}
	}

	public void Idle()
	{
		if (countForward < idleThreshold && countBackward < idleThreshold)
		{
			rb.velocity = new Vector2(0, idleSpeed);
			countForward++;
		}
		else if (countForward >= idleThreshold && countBackward < idleThreshold)
		{
			rb.velocity = new Vector2(0, -idleSpeed);
			countBackward++;
		}
		else
		{
			countForward = 0; countBackward=0;
		}
		
	}

	public void Walk()
	{
		rb.velocity = walkSpeed * walkDirection2.normalized;
	}

	public void WalkStageOne()
	{
		rb.velocity = walkSpeed * walkDirection1.normalized;
	}

}
