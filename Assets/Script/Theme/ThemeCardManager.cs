using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemeCardManager : MonoBehaviour
{
	public ThemeCard[] cards;
	[SerializeField] private Sprite unknownImage;
	[SerializeField] private SpriteRenderer[] images;
	[SerializeField] private TextMeshProUGUI[] timeTexts;

	private void OnEnable()
	{
		DisplayCardAndTime();
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
				if (card.isFinished)
					timeTexts[i].text = string.Format("{0:0}:{1:00}", 0, card.timeUsed);
				else
					timeTexts[i].text = "--:--";
			}
			else
			{
				// image
				images[i].sprite = unknownImage;
			}
		}
	}
}
