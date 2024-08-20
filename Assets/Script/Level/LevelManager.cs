using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private int currentLevelIndex = 0;

    private void Start()
    {
        // Optional: Load the first level automatically when the game starts
        LoadCurrentLevel();
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= themes[currentThemeIndex].levels.Count)
        {
            currentLevelIndex = 0; // Reset level index

            currentThemeIndex++;
            if (currentThemeIndex >= themes.Count)
            {
                currentThemeIndex = 0; // Reset theme index if we have finished all themes
                Debug.Log("All themes and levels completed. Restarting from the first theme and level.");
            }
        }

        LoadCurrentLevel();
    }

    public void ReloadCurrentLevel()
    {
        LoadCurrentLevel();
    }

    private void LoadCurrentLevel()
    {
        if (currentThemeIndex < themes.Count && currentLevelIndex < themes[currentThemeIndex].levels.Count)
        {
            string levelToLoad = themes[currentThemeIndex].levels[currentLevelIndex];
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            Debug.LogError("Invalid theme or level index. Cannot load level.");
        }
    }

    public void LoadSpecificLevel(int themeIndex, int levelIndex)
    {
        if (themeIndex < themes.Count && levelIndex < themes[themeIndex].levels.Count)
        {
            currentThemeIndex = themeIndex;
            currentLevelIndex = levelIndex;
            LoadCurrentLevel();
        }
        else
        {
            Debug.LogError("Invalid theme or level index. Cannot load specific level.");
        }
    }

    // Optional: Helper methods to get current theme and level indices
    public int GetCurrentThemeIndex()
    {
        return currentThemeIndex;
    }

    public int GetCurrentLevelIndex()
    {
        return currentLevelIndex;
    }
}
