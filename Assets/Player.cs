using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string musicName;

	MusicManager musicManager;

	private void Awake()
	{
		musicManager = GetComponent<MusicManager>();
	}
	private void Start()
	{
		musicManager.PlayMusic(musicName);
	}
}
