using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCardManager : MonoBehaviour
{
	public LevelCard[] cards;
    [SerializeField] private Sprite unknownImage;
    [SerializeField] private Image[] images;
    [SerializeField] private GameObject[] stamps;
    [SerializeField] private TextMeshProUGUI[] timeTexts;

	private void OnEnable()
	{
		DisplayCardAndTime();
		AddStamps();
	}

	public void DisplayCardAndTime()
    {
		for (int i = 0; i < cards.Length; i++)
		{
			var card = cards[i];
			if (card.isVisited)
			{
				// image
				images[i].sprite = card.image;
				// time
				timeTexts[i].enabled = true;
				timeTexts[i].text = string.Format("{0:0}:{1:00}", 0, card.timeUsed);
			}
			else
			{
				// image
				images[i].sprite = unknownImage;
			}
		}
    }

	public void AddStamps()
	{
		for (int i = 0; i < cards.Length; i++)
		{
			if (cards[i].isFinished)
			{
				stamps[i].SetActive(true);
			}
		}
	}
}
