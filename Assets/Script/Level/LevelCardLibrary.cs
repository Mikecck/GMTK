using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct LevelCard
{
	public int themeId;
	public int levelId;
	public string name;
	public string displayName;
	public RawImage image;
	public RawImage displayImage;
	public bool isVisited; // isVisited ? show image & name : hide image & name
	public bool isFinished; // isFinished ? stamp : none
	public int timeUsed;

	public int GetCardIndex()
	{
		return (themeId - 1) * 5 + levelId - 1;
	}
}
public class LevelCardLibrary : MonoBehaviour
{
	public static LevelCardLibrary Instance;

	public LevelCard[] levelCards;
	[SerializeField] private RawImage mysteryImage;
	[SerializeField] private string mysteryName = "???";

	public void CheckVisibility()
	{
		for (int i = 0; i < levelCards.Length; ++i)
		{
			LevelCard card = levelCards[i];
			if (!card.isVisited)
			{
				card.displayImage = mysteryImage;
				card.displayName = mysteryName;
			}
			else
			{
				card.displayImage = card.image;
				card.displayName = card.name;
			}
		}
	}

}
