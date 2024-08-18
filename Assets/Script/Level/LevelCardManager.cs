using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCardManager : MonoBehaviour
{
	public static LevelCardManager Instance;

	[SerializeField] private LevelCardLibrary levelCardLibrary;
	[SerializeField] private RawImage displayImage;
	[SerializeField] private TextMeshProUGUI displayName;
	[SerializeField] private TextMeshProUGUI timeUsed;
	[SerializeField] private RawImage finishStamp;


	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		RenderCards();
	}

	private void RenderCards()
	{
		for (int i = 0; i < levelCardLibrary.levelCards.Length; i++) {
			LevelCard card = levelCardLibrary.levelCards[i];
			displayImage = card.displayImage;
			displayName.text = card.displayName;

			if (card.isVisited) // show time
			{
				timeUsed.text = string.Format("{0:0}:{1:00}", 0, card.timeUsed);
				timeUsed.enabled = true;
			}

			if (card.isFinished) // show stamp
			{
				finishStamp.enabled = true;
			}
		}

	}

}
