using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Theme Card", menuName = "Theme Card")]
public class ThemeCard : ScriptableObject
{
	public int themeId;
	public Sprite image;
	// isVisited ? show image & enable animator : show blank image & disable animator
	public bool isVisited;
	// isFinished ? show timeUsed : show --:--
	public bool isFinished;
	public int timeUsed;
}
