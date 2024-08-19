using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThemeSelector : MonoBehaviour
{


	public void Theme1Selected()
	{
		SceneManager.LoadScene("T1L1");
	}
	public void Theme2Selected()
	{
		SceneManager.LoadScene("T2L1");
	}

}
