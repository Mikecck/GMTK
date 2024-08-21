using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFree : MonoBehaviour
{
    public GameObject targetFence;
	public SheepControl[] sheepControls;
	public void SetFreeOrNot()
	{
		if (targetFence.activeSelf)
		{
			for (int i = 0; i < sheepControls.Length; i++)
			{
				sheepControls[i].isIdle = false;
			}
		}
	}
}
