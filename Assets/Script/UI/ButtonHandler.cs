using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour
{
    public UIManager uiManager;
    private float winAnimationDuration = 0.7f;

    [SerializeField] private int themeIndex;
    [SerializeField] private int levelIndex;

    public void OnButtonClick()
    {
        UIManager uIManager = FindObjectOfType<UIManager>();
        uiManager.PlayShootEffect();

        uiManager.PlayWinAnimation();

        StartCoroutine(WaitAndLoadNextLevel());
    }

    private IEnumerator WaitAndLoadNextLevel()
    {
        yield return new WaitForSeconds(winAnimationDuration);
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.LoadLevel(themeIndex, levelIndex);
        }
        else
        {
            Debug.LogError("LevelManager not found in the scene!");
        }
    }
}