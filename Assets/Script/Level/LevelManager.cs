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

    [SerializeField] private int currentThemeIndex = 0;
    [SerializeField] private int currentLevelIndex = 0;

    public void LoadNextLevel()
    {
        if (themes == null || themes.Count == 0)
        {
            Debug.LogError("No themes or levels set in the LevelManager.");
            return;
        }

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
        currentLevelIndex++;
    }

    private void LoadLevel(int themeIndex, int levelIndex)
    {
        string levelName = $"T{themeIndex + 1}L{levelIndex + 1}";
        Debug.Log($"Loading level: {levelName} from theme: {themes[themeIndex].themeName}");
        SceneManager.LoadScene(levelName);
    }
    public void ResetLevels()
    {
        currentThemeIndex = 0;
        currentLevelIndex = 0;
        LoadNextLevel(); 
    }
}
