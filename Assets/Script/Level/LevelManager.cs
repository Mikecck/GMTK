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

    [SerializeField] private int currentThemeIndex = 0;
    [SerializeField] private int currentLevelIndex = 0;

    private bool isLevelLoading = false;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        LoadCurrentLevel();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isLevelLoading = false;
    }

    public void LoadCurrentLevel()
    {
        if (isLevelLoading) return;

        isLevelLoading = true;

        if (currentThemeIndex < themes.Count && currentLevelIndex < themes[currentThemeIndex].levels.Count)
        {
            string levelToLoad = themes[currentThemeIndex].levels[currentLevelIndex];
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            Debug.LogError("Invalid theme or level index. Cannot load level.");
            isLevelLoading = false;
        }
    }

    public void LoadNextLevel()
    {
        Debug.Log("LoadNextLevel!!");
        if (isLevelLoading) return;

        int tempThemeIndex = currentThemeIndex;
        int tempLevelIndex = currentLevelIndex + 1;

        if (tempLevelIndex >= themes[tempThemeIndex].levels.Count)
        {
            tempLevelIndex = 0;
            tempThemeIndex++;

            if (tempThemeIndex >= themes.Count)
            {
                tempThemeIndex = 0;
            }
        }

        // Load the next level
        if (tempThemeIndex < themes.Count && tempLevelIndex < themes[tempThemeIndex].levels.Count)
        {
            isLevelLoading = true;
            string levelToLoad = themes[tempThemeIndex].levels[tempLevelIndex];
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            Debug.LogError("Invalid theme or level index. Cannot load the next level.");
        }
    }

    public void ReloadCurrentLevel()
    {
        if (!isLevelLoading)
        {
            LoadCurrentLevel();
        }
    }
    public int GetCurrentThemeIndex()
    {
        return currentThemeIndex;
    }

    public int GetCurrentLevelIndex()
    {
        return currentLevelIndex;
    }
}
