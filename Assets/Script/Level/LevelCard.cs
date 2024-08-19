using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Level Card", menuName = "Level Card")]
public class LevelCard : ScriptableObject
{
	public int themeId;
	public int levelId;
	public Sprite image;
	[HideInInspector] public bool isVisited; // isVisited ? show card image : show unknown image
	[HideInInspector] public bool isFinished; // isFinished ? show stamp : hide stamp
	[HideInInspector] public int timeUsed;
}
