using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private ScalableObjectController currentSelectedObject;
    [SerializeField]
    private List<ScalableObjectController> scalableTilemaps; // List to hold tilemap controllers

    [SerializeField]
    private float timeLimit = 15f;  // Time limit in seconds for the current level

    private float timeRemaining;
    private bool levelComplete = false;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartLevelTimer();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndAssignScalableTilemaps();
        StartLevelTimer();
    }

    void Update()
    {
        if (!levelComplete)
        {
            UpdateTimer();
        }
    }

    void StartLevelTimer()
    {
        timeRemaining = timeLimit;
        levelComplete = false;
    }

    void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                Debug.Log("Time's up! Proceeding to the next level.");
                FailLevel();
            }
        }
    }

    void FindAndAssignScalableTilemaps()
    {
        scalableTilemaps = new List<ScalableObjectController>(FindObjectsOfType<ScalableObjectController>());
    }

    public void SelectObject(ScalableObjectController obj)
    {
        if (currentSelectedObject != null)
        {
            currentSelectedObject.isSelected = false;
        }
        currentSelectedObject = obj;
        currentSelectedObject.isSelected = true;
    }

    public void DeselectCurrentObject()
    {
        if (currentSelectedObject != null)
        {
            currentSelectedObject.isSelected = false;
            currentSelectedObject = null;
        }
    }

    public void NotifyTilemapCorrect(ScalableObjectController obj)
    {
        Debug.Log("Correct tilemap enabled: " + obj.name);
        CheckForLevelCompletion();
    }

    private void CheckForLevelCompletion()
    {
        bool allCorrect = true;
        foreach (var tilemapController in scalableTilemaps)
        {
            if (!tilemapController.IsCorrectTilemapActive())
            {
                allCorrect = false;
                break;  // Exit early if any tilemap is not correct
            }
        }

        if (allCorrect)
        {
            levelComplete = true;
            Debug.Log("All correct tilemaps are active within time, awarding badge and proceeding to the next level.");
            AwardBadge();
            LevelManager.Instance.LoadNextLevel();  // Load the next level
        }
        else
        {
            Debug.Log("Not all correct tilemaps are active yet.");
        }
    }

    private void AwardBadge()
    {
        int currentTheme = LevelManager.Instance.CurrentThemeIndex;
        int currentLevel = LevelManager.Instance.CurrentLevelIndex;
        BadgeManager.Instance.AwardBadge(currentTheme, currentLevel);
    }

    private void FailLevel()
    {
        levelComplete = true;
        Debug.Log("Level failed, proceeding to the next level without awarding badge.");
        LevelManager.Instance.LoadNextLevel();  // Load the next level
    }
}
