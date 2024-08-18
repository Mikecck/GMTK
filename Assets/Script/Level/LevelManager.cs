using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<Theme> themes;

    public delegate bool WinningConditionDelegate();
    public WinningConditionDelegate OnWinningConditionCheck;

    public void LoadCurrentLevel(int themeIndex, int levelIndex)
    {
        if (themeIndex < themes.Count && levelIndex < themes[themeIndex].levels.Count)
        {
            // Exception check
            if (themeIndex >= themes.Count || levelIndex >= themes[themeIndex].levels.Count)
            {
                Debug.LogError($"Level index out of bounds! ThemeIndex: {themeIndex}, LevelIndex: {levelIndex}");
                return;
            }

            string sceneName = themes[themeIndex].levels[levelIndex].sceneName;
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError("Scene name is null or empty");
            }
        }
        else
        {
            Debug.LogError("Level index out of bounds");
        }
    }

    public int GetLevelCount(int themeIndex)
    {
        if (themeIndex < themes.Count)
        {
            return themes[themeIndex].levels.Count;
        }
        Debug.LogWarning("Theme index out of bounds!");
        return 0;
    }

    public bool CheckWinningCondition(int themeIndex, int levelIndex)
    {
        if (themeIndex >= themes.Count || levelIndex >= themes[themeIndex].levels.Count)
        {
            Debug.LogError($"Winning condition check failed! ThemeIndex: {themeIndex}, LevelIndex: {levelIndex} are out of bounds.");
            return false;
        }

        foreach (ScalableObjectInfo info in themes[themeIndex].levels[levelIndex].scalableObjects)
        {
            if (info == null || info.scalableObject == null)
            {
                Debug.LogError("ScalableObjectController is null. Check if you proper assign corresponding object in editor.");
                continue;
            }
            if (info.scalableObject.GetCurrentState() != info.targetState)
            {
                return false;
            }
        }
        return true;
    }

}

[System.Serializable]
public class Theme
{
    public string themeName;
    public List<Level> levels;
}

[System.Serializable]
public class Level
{
    public string sceneName;
    public List<ScalableObjectInfo> scalableObjects;
}

[System.Serializable]
public class ScalableObjectInfo
{
    public ScalableObjectController scalableObject;
    public State targetState;
}