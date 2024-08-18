using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[Serializable]
public class Level
{
    public string sceneName;
}

[Serializable]
public class Theme
{
    public List<Level> levels;
}

public class LevelManager : Singleton<LevelManager>
{
    public List<Theme> themes;

    [SerializeField]
    private int currentThemeIndex = 0;
    [SerializeField]
    private int currentLevelIndex = 0;

    public void LoadCurrentLevel()
    {
        if (IsValidLevel(currentThemeIndex, currentLevelIndex))
        {
            string sceneName = themes[currentThemeIndex].levels[currentLevelIndex].sceneName;
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError($"Scene name is empty or null! ThemeIndex: {currentThemeIndex}, LevelIndex: {currentLevelIndex}");
            }
        }
        else
        {
            Debug.LogError($"Level index out of bounds! ThemeIndex: {currentThemeIndex}, LevelIndex: {currentLevelIndex}");
        }
    }

    public void LoadNextLevel()
    {
        if (IsValidLevel(currentThemeIndex, currentLevelIndex + 1))
        {
            currentLevelIndex++;
            LoadCurrentLevel();
        }
        else if (IsValidTheme(currentThemeIndex + 1))
        {
            currentThemeIndex++;
            currentLevelIndex = 0;
            LoadCurrentLevel();
        }
        else
        {
            Debug.Log("No more levels to load! Completed all themes.");
        }
    }


    private bool IsValidLevel(int themeIndex, int levelIndex)
    {
        return themeIndex < themes.Count && levelIndex < themes[themeIndex].levels.Count;
    }

    private bool IsValidTheme(int themeIndex)
    {
        return themeIndex < themes.Count;
    }

    public void SetCurrentLevel(int themeIndex, int levelIndex)
    {
        if (IsValidLevel(themeIndex, levelIndex))
        {
            currentThemeIndex = themeIndex;
            currentLevelIndex = levelIndex;
        }
        else
        {
            Debug.LogError("Invalid theme or level index provided.");
        }
    }
}
