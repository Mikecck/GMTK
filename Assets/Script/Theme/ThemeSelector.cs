using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThemeSelector : MonoBehaviour
{
	public GameObject[] themes;
	public ThemeCardManager themeCardManager;
	public void PlayTargetThemeAnimation(int themeId)
	{
		if (themeCardManager.cards[themeId - 1].isVisited)
		{
			themes[themeId - 1].GetComponent<Animator>().SetBool("play", true);
		}
	}

	public void ExitAllThemeAnimation()
	{
		for (int i = 0; i < themes.Length; i++)
		{
			Animator anim = themes[i].GetComponent<Animator>();
			anim.SetBool("play", false);
			anim.Rebind();
			anim.Update(0f);
		}
	}

	public void Theme1Selected()
	{
		SceneManager.LoadScene("T1L1");
	}
	public void Theme2Selected()
	{
		if (themeCardManager.cards[1].isVisited)
			SceneManager.LoadScene("T2L1");
	}

}
