using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWalk : MonoBehaviour
{
	public GameObject flamingo1;
	public GameObject flamingo2;
	public bool canWalk;

	private void Update()
	{
		if (flamingo1.transform.position.x > -13)
			canWalk = flamingo1.activeSelf && flamingo2.activeSelf;
		else
			canWalk = false;
	}
	
}
