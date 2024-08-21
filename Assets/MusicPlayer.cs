using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public MusicName musicName;

	public enum MusicName
	{
		MainMenu,
		ThemeWatermelon,
		ThemeAnimal
	}
	private void Start()
	{
		MusicManager.Instance.PlayMusic(musicName.ToString());
	}
	private void Update()
	{
		if (MusicManager.Instance.curMusic != musicName)
		{
			MusicManager.Instance.PlayMusic(musicName.ToString());
			MusicManager.Instance.curMusic = musicName;
		}
	}
}
