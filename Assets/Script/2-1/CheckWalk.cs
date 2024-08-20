using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckWalk : MonoBehaviour
{
	public GameObject flamingo1;
	public GameObject flamingo2;
	public bool canWalk;
    [SerializeField]
    private Button shootButton;

    private void Update()
	{
        if (flamingo1.transform.position.x > -13)
        {
            canWalk = flamingo1.activeSelf && flamingo2.activeSelf;
            shootButton.gameObject.SetActive(true);
        }
        else
        {
            canWalk = false;
            shootButton.gameObject.SetActive(false);
        }
    }
	
}
