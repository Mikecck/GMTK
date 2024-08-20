using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    private ScalableObjectController currentSelectedObject;
    [SerializeField]
    private List<ScalableObjectController> scalableTilemaps;

    [SerializeField]
    private float timeLimit = 15f;

    private float timeRemaining;
    private bool levelComplete = false;

    [SerializeField]
    private Button blockingObject;

    [SerializeField]
    private Button levelButton;

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

        if (blockingObject == null)
        {
            blockingObject = GameObject.Find("Button_Block").GetComponent<Button>();
        }

        if (levelButton == null)
        {
            levelButton = GameObject.Find("Button_Checkout").GetComponent<Button>();
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
                Debug.Log("Time's up!");
                //FailLevel();
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
            Debug.Log("All correct tilemaps are active within time, awarding badge.");
            DisableBlockingObject();
            AwardBadge();
        }

    }

    private void DisableBlockingObject()
    {
        if (blockingObject != null)
        {
            blockingObject.gameObject.SetActive(false); // Disable the blocking object to make the button clickable
            levelButton.interactable = true; // Ensure the button is interactable
        }
    }

    private void AwardBadge()
    {
        int currentTheme = LevelManager.Instance.levelCards[LevelManager.Instance.CurrentLevelIndex].themeId;
        int currentLevel = LevelManager.Instance.levelCards[LevelManager.Instance.CurrentLevelIndex].levelId;
        BadgeManager.Instance.AwardBadge(currentTheme, currentLevel);
    }

    private void FailLevel()
    {
        //levelComplete = true;
        levelButton.interactable = true;
        DisableBlockingObject();
    }

    // Level Complete Helper
    public bool LevelComplete
    {
        get { return levelComplete; }
        set { levelComplete = value; }
    }

}
