using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<Theme> themes;

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
                Debug.LogError("Scene name is null or empty");
            }
        }
        else
        {
            Debug.LogError($"Level index out of bounds! ThemeIndex: {themeIndex}, LevelIndex: {levelIndex}");
        }
    }

    public int GetLevelCount(int themeIndex)
    {
        if (themeIndex < themes.Count)
        {
            Debug.Log("Level count: " + themes[themeIndex].levels.Count);
            return themes[themeIndex].levels.Count;
        }
        Debug.LogWarning("Theme index out of bounds!");
        return 0;
    }

    public bool CheckWinningCondition(int themeIndex, int levelIndex, ScalableObjectController scalableObject, float targetScaleMultiplier)
    {
        if (!IsValidLevel(themeIndex, levelIndex))
        {
            Debug.LogError($"Winning condition check failed! ThemeIndex: {themeIndex}, LevelIndex: {levelIndex} are out of bounds.");
            return false;
        }

        if (scalableObject == null)
        {
            Debug.LogError("ScalableObjectController is null. Check if you properly assign the object in the editor.");
            return false;
        }

        // Check if the current scale multiplier matches the target scale multiplier
        return Mathf.Approximately(scalableObject.GetCurrentScaleMultiplier(), targetScaleMultiplier);
    }

    // Check if the current level is completed (Helper)
    private bool IsValidLevel(int themeIndex, int levelIndex)
    {
        return themeIndex < themes.Count && levelIndex < themes[themeIndex].levels.Count;
    }
}

[System.Serializable]
public class Theme
{
    [SerializeField] public string themeName;
    [SerializeField] public List<Level> levels;
}

[System.Serializable]
public class Level
{
    [SerializeField] public string sceneName;
    [SerializeField] public List<ScalableObjectController> scalableObjects;
}
