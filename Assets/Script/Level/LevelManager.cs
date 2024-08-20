using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public List<LevelCard> levelCards;
    public int CurrentThemeIndex { get; private set; } = 0;
    public int CurrentLevelIndex { get; private set; } = 0;

    public void LoadLevel(int themeIndex, int levelIndex)
    {
        int adjustedThemeIndex = themeIndex + 1;
        int adjustedLevelIndex = levelIndex + 1;

        string sceneName = $"T{adjustedThemeIndex}L{adjustedLevelIndex}";

        LevelCard levelCard = levelCards.Find(card => card.themeId == adjustedThemeIndex && card.levelId == adjustedLevelIndex);

        if (levelCard != null)
        {
            CurrentThemeIndex = themeIndex;
            CurrentLevelIndex = levelIndex;

            if (Application.CanStreamedLevelBeLoaded(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError($"Scene {sceneName} could not be found or loaded.");
            }
        }
        else
        {
            Debug.LogError("Level card not found");
        }
    }


    public void LoadNextLevel()
    {
        int nextLevelIndex = CurrentLevelIndex + 1;

        if (nextLevelIndex < 5)
        {
            LoadLevel(CurrentThemeIndex, nextLevelIndex);
        }
        else
        {
            int nextThemeIndex = CurrentThemeIndex + 1;
            if (nextThemeIndex < 2)
            {
                LoadLevel(nextThemeIndex, 0);
            }
            else
            {
                Debug.Log("All levels completed!");
            }
        }
    }

    public void ReloadCurrentLevel()
    {
        LoadLevel(CurrentThemeIndex, CurrentLevelIndex);
    }
}
