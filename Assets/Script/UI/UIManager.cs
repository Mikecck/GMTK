using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsPanel;
	[SerializeField] private GameObject themesPanel;

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
		LevelManager.Instance.LoadCurrentLevel(0, 0);
	}
	public void Theme2Selected()
	{
		LevelManager.Instance.LoadCurrentLevel(1, 0);
	}
	public void Theme3Selected()
	{
		LevelManager.Instance.LoadCurrentLevel(2, 0);
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

