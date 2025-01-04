using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : Singleton<LevelManager>
{
    [System.Serializable]
    public class Theme
    {
        public string themeName;
        public List<string> levels;
    }

    [SerializeField]
    private List<Theme> themes;

    private int currentThemeIndex = 0;
    private int currentLevelIndex = -1;
    
    private bool isFirstPlay = true;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void LoadNextLevel()
    {
        if (themes == null || themes.Count == 0)
        {
            Debug.LogError("No themes or levels set in the LevelManager.");
            return;
        }

        currentLevelIndex++;

        if (currentLevelIndex >= themes[currentThemeIndex].levels.Count)
        {
            currentLevelIndex = 0;
            currentThemeIndex++;

            if (currentThemeIndex >= themes.Count)
            {
                Debug.Log("You've completed all themes and levels!");
                currentThemeIndex = 0;
                return;
            }
        }

        LoadLevel(currentThemeIndex, currentLevelIndex);
    }

    public void LoadLevel(int themeIndex, int levelIndex)
    {
        string levelName = $"T{themeIndex + 1}L{levelIndex + 1}";
        SceneManager.LoadScene(levelName);
    }

    public void ResetLevels()
    {
        Debug.Log("Resetting level progression...");
        currentThemeIndex = 0;
        currentLevelIndex = -1;
        isFirstPlay = true;
        LoadNextLevel();
    }

    public void OnMainMenuReturn()
    {
        Debug.Log("Returning to main menu - Resetting progression state");
        ResetLevels();
    }

    // Debug levels
    public string GetCurrentLevelInfo()
    {
        return $"Current Theme: {currentThemeIndex}, Current Level: {currentLevelIndex}, First Play: {isFirstPlay}";
    }
}