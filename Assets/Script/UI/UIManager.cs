using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private TextMeshProUGUI bgmValueText;
    [SerializeField] private Slider sfxSlider;
	[SerializeField] private TextMeshProUGUI sfxValueText;


	public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

	private void Start()
	{
        ChangeBGMValueText();
        ChangeSFXValueText();
	}

	public void ToggleSettingsMenu()
    {
        if (!settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(true);
            mainMenu.SetActive(false);
            ZoomInCam();
		}
        else
        {
			settingsMenu.SetActive(false);
			mainMenu.SetActive(true);
			ZoomOutCam();
		}
    }
    public void ZoomInCam()
    {
		cam.GetComponent<Animator>().SetBool("zoomIn", true);
    }

	public void ZoomOutCam()
	{
		cam.GetComponent<Animator>().SetBool("zoomIn", false);
	}

    public void ChangeBGMValueText()
    {
        bgmValueText.text = bgmSlider.value.ToString();
    }
	public void ChangeSFXValueText()
	{
		sfxValueText.text = sfxSlider.value.ToString();
	}
}

