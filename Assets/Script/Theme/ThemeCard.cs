using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Theme Card", menuName = "Theme Card")]
public class ThemeCard : ScriptableObject
{
	public Sprite image;
	// isVisited ? show image & enable animator : show blank image & disable animator
	[HideInInspector] public bool isVisited;
	// isFinished ? show timeUsed : show --:--
	[HideInInspector] public bool isFinished;
	[HideInInspector] public int timeUsed;
}
