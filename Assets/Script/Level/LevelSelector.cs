using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
	[NonSerialized] public int themeId;

	public GameObject levelCards;
	public GameObject scrollbar;
	public GameObject levelIndicators;
	public Sprite redIndicator;
	public Sprite whiteIndicator;

	private float scrollPos = 0;
	float[] pos;
	private float distance;

	public int levelId;

	private void Start()
	{
		Prepare();
	}

	private void OnEnable()
	{
		scrollPos = 0;
	}

	private void Update()
	{
		Swipe();
	}

	private void Prepare()
	{
		pos = new float[levelCards.transform.childCount];
		distance = 1f / (pos.Length - 1f);

		for (int i = 0; i < pos.Length; i++)
		{
			pos[i] = distance * i;
		}
	}

	private void Swipe()
	{
		if (Input.GetMouseButton(0))
		{
			scrollPos = scrollbar.GetComponent<Scrollbar>().value;
		}
		else
		{
			for (int i = 0; i < pos.Length; i++)
			{
				if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
				{
					scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
				}
			}
		}

		Effect();
	}


	public void JumpTo(bool toRight)
	{
		int step = 0;
		if(toRight) step = 1;
		else step = -1;

		scrollPos = scrollbar.GetComponent<Scrollbar>().value + distance * step;

		for (int i = 0; i < pos.Length; i++)
		{
			if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
			{
				scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
			}
		}

		Effect();
	}

	public void JumpTo(int targetLevel)
	{
		int step = targetLevel - GetCurrentLevel();

		scrollPos = scrollbar.GetComponent<Scrollbar>().value + distance * step;

		for (int i = 0; i < pos.Length; i++)
		{
			if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
			{
				scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
			}
		}

		Effect();
	}

	private int GetCurrentLevel()
	{
		scrollPos = scrollbar.GetComponent<Scrollbar>().value;
		for (int i = 0; i < pos.Length; i++)
		{
			if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
			{
				return i + 1;
			}
		}

		return 0;
	}

	private void Effect()
	{
		for (int i = 0; i < pos.Length; i++)
		{
			if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
			{
				levelCards.transform.GetChild(i).localScale = Vector2.Lerp(levelCards.transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
				levelIndicators.transform.GetChild(i).GetComponent<Image>().sprite = redIndicator;
				for (int j = 0; j < pos.Length; j++)
				{
					if (j != i)
					{
						levelIndicators.transform.GetChild(j).GetComponent<Image>().sprite = whiteIndicator;
						levelCards.transform.GetChild(j).localScale = Vector2.Lerp(levelCards.transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
					}
				}
			}
		}
	}

}
