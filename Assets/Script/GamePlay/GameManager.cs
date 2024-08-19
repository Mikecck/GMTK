using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private ScalableObjectController currentSelectedObject;
    [SerializeField]
    private List<ScalableObjectController> scalableObjects;

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
        FindAndAssignScalableObjects();
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

    void FindAndAssignScalableObjects()
    {
        scalableObjects = new List<ScalableObjectController>(FindObjectsOfType<ScalableObjectController>());
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

    public void NotifySpriteCorrect(ScalableObjectController obj)
    {
        Debug.Log("Correct sprite enabled: " + obj.name);
        CheckForLevelCompletion();
    }

    private void CheckForLevelCompletion()
    {
        bool allCorrect = true;
        foreach (var scalableObject in scalableObjects)
        {
            if (!scalableObject.IsCorrectSpriteActive())
            {
                allCorrect = false;
                break;  // Exit early if any object is not correct
            }
        }

        if (allCorrect)
        {
            levelComplete = true;
            Debug.Log("All correct sprites are active within time, awarding badge and proceeding to the next level.");
            AwardBadge();
            LevelManager.Instance.LoadNextLevel();  // Load the next level
        }
        else
        {
            Debug.Log("Not all correct sprites are active yet.");
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
