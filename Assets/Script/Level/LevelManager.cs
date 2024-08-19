using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public LevelCard[] levelCards;

    [SerializeField]
    private LevelSelector levelSelector;

    [SerializeField]
    private LevelCardManager levelCardManager;

    private int currentLevelIndex = 0;

    public int CurrentLevelIndex
    {
        get { return currentLevelIndex; }
        set
        {
            if (IsValidLevel(value))
            {
                currentLevelIndex = value;
                LoadCurrentLevel();
            }
        }
    }

    private void Start()
    {
        LoadCurrentLevel();
        InitializeLevelSelector();
    }

    private void InitializeLevelSelector()
    {
        // Set up the LevelSelector with the current levels
        levelSelector.themeId = levelCards[currentLevelIndex].themeId;
        levelSelector.levelId = currentLevelIndex + 1;
    }

    public void LoadCurrentLevel()
    {
        if (IsValidLevel(currentLevelIndex))
        {
            string sceneName = GenerateSceneName(levelCards[currentLevelIndex]);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Invalid level index: " + currentLevelIndex);
        }
    }

    public void LoadNextLevel()
    {
        if (IsValidLevel(currentLevelIndex + 1))
        {
            CurrentLevelIndex++;
        }
        else
        {
            Debug.Log("No more levels to load, reached end of array.");
        }
    }

    public void LoadPreviousLevel()
    {
        if (IsValidLevel(currentLevelIndex - 1))
        {
            CurrentLevelIndex--;
        }
        else
        {
            Debug.Log("Already at the first level.");
        }
    }

    public void SetCurrentLevel(int index)
    {
        if (IsValidLevel(index))
        {
            CurrentLevelIndex = index;
        }
        else
        {
            Debug.LogError("Invalid level index set: " + index);
        }
    }

    private bool IsValidLevel(int index)
    {
        return index >= 0 && index < levelCards.Length;
    }

    private string GenerateSceneName(LevelCard levelCard)
    {
        return $"T{levelCard.themeId}L{levelCard.levelId}";
    }

    public void OnLevelSelected(int levelIndex)
    {
        SetCurrentLevel(levelIndex);
    }

    public void RefreshLevelCards()
    {
        // This method could be called whenever you need to update the display of level cards
        levelCardManager.DisplayCardAndTime();
        levelCardManager.AddStamps();
    }
}
