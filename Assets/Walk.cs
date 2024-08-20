using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walk : MonoBehaviour
{
	public float walkSpeed;

	Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		Move();
	}
	
	public void Move()
	{
		rb.velocity = new Vector2(rb.velocity.y, walkSpeed);
	}
}
