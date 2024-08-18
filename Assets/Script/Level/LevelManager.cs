using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[Serializable]
public class Level
{
    public string sceneName; // The name of the scene in Unity
}

[Serializable]
public class Theme
{
    public List<Level> levels; // List of levels in this theme
}

public class LevelManager : Singleton<LevelManager>
{
    public List<Theme> themes; // This will be editable in the Unity editor

    public void LoadCurrentLevel(int themeIndex, int levelIndex)
    {
        if (IsValidLevel(themeIndex, levelIndex))
        {
            string sceneName = themes[themeIndex].levels[levelIndex].sceneName;
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError($"Scene name is empty or null! ThemeIndex: {themeIndex}, LevelIndex: {levelIndex}");
            }
        }
        else
        {
            Debug.LogError($"Level index out of bounds! ThemeIndex: {themeIndex}, LevelIndex: {levelIndex}");
        }
    }

    private bool IsValidLevel(int themeIndex, int levelIndex)
    {
        return themeIndex < themes.Count && levelIndex < themes[themeIndex].levels.Count;
    }
}
