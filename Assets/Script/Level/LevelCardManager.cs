using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCardManager : MonoBehaviour
{
	[SerializeField] private LevelCard[] cards;
    [SerializeField] private Sprite unknownImage;
    [SerializeField] private RawImage[] images;
    [SerializeField] private GameObject[] stamps;
    [SerializeField] private TextMeshProUGUI[] timeTexts;

	public void DisplayCard()
    {

    }
}
