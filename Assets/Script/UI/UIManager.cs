using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager: MonoBehaviour
{
	[Header("For Main Menu Scene")]
	[SerializeField] private bool isMainMenu;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsPanel;
	[SerializeField] private GameObject themesPanel;
	[SerializeField] private Slider bgmSlider;
    [SerializeField] private TextMeshProUGUI bgmValueText;
    [SerializeField] private Slider sfxSlider;
	[SerializeField] private TextMeshProUGUI sfxValueText;

	[Header("For Game Scenes")]
	[SerializeField] private bool isHud;
	[SerializeField] private int maxTime = 15;
	[SerializeField] private TextMeshProUGUI elapsedTimeText;
	private float elapsedTime;
	private int seconds;
	[SerializeField] private GameObject inGameMenu;


	private void Start()
	{
		if (bgmSlider && sfxSlider)
		{
			ChangeBGMValueText();
			ChangeSFXValueText();
		}
        
	}

	private void Update()
	{
		if (isHud)
		{
			elapsedTime += Time.deltaTime;
			seconds = Mathf.FloorToInt(elapsedTime % 60);
			if (seconds <= maxTime)
				elapsedTimeText.text = string.Format("{0:0}:{1:00}", 0, seconds);
		}
		
	}

	public void ToggleSettingsPanel()
    {
        if (!settingsPanel.activeSelf)
        {
			settingsPanel.SetActive(true);
            mainMenu.SetActive(false);
            //ZoomInCam();
		}
        else
        {
			settingsPanel.SetActive(false);
			mainMenu.SetActive(true);
			//ZoomOutCam();
		}
    }
	public void ToggleThemesPanel()
	{
		if (!themesPanel.activeSelf)
		{
			themesPanel.SetActive(true);
			mainMenu.SetActive(false);
		}
		else
		{
			themesPanel.SetActive(false);
			mainMenu.SetActive(true);
		}
	}

	public void Theme1Selected()
	{
		ToggleThemesPanel();
		SceneManager.LoadScene("T1L1");
	}
	public void Theme2Selected()
	{
		ToggleThemesPanel();
		SceneManager.LoadScene("T2L1");
	}
	public void Theme3Selected()
	{
		ToggleThemesPanel();
		SceneManager.LoadScene("T3L1");
	}

	public void ToggleInGameMenu()
	{
		if (!inGameMenu.activeSelf)
		{
			// pause timer
			isHud = false;
			// open menu
			inGameMenu.SetActive(true);
		}
		else
		{
			// resume timer
			isHud=true;
			// close menu
			inGameMenu.SetActive(false);
		}
		
	}

	public void ToggleSettingsPanelInGame()
	{
		if (!settingsPanel.activeSelf)
		{
			inGameMenu.SetActive(false);
			settingsPanel.SetActive(true);
		}
		else
		{
			inGameMenu.SetActive(true);
			settingsPanel.SetActive(false);
		}
	}

	public void InGame2MainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void InGame2Settings()
	{
		ToggleSettingsPanelInGame();
	}

	public void InGame2LevelSelection()
	{
		SceneManager.LoadScene("LevelSelection");
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

	public void QuitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}

