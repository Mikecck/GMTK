using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamingoControl : MonoBehaviour
{
	public CheckWalk checker;
	public float walkSpeed;
	public Vector2 walkDirection1;
	public Vector2 walkDirection2;
	public Vector2 walkDirection3;
	public int stageOneTime;
	public int stageTwoTime;

	Rigidbody2D rb;
	int countStageOne;
	int countStageTwo;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (checker.canWalk)
		{
			if (countStageOne < stageOneTime && 
				countStageTwo < stageTwoTime)
			{
				countStageOne++;
				WalkStageOne();
			}
			else if (countStageTwo < stageTwoTime)
			{
				countStageTwo++;
				WalkStageTwo();
			}
			else
			{
				WalkStageThree();
			}
		}
		else Stop();
	}

	public void Stop()
	{
		rb.velocity = Vector2.zero;
	}
	public void WalkStageThree()
	{
		rb.velocity = walkSpeed * walkDirection3.normalized;
	}

	public void WalkStageTwo()
	{
		rb.velocity = walkSpeed * walkDirection2.normalized;
	}

	public void WalkStageOne()
	{
		rb.velocity = walkSpeed * walkDirection1.normalized;
	}
}
