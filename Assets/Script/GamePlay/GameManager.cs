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
            blockingObject = GameObject.Find("Shoot_Block").GetComponent<Button>();
        }

        if (levelButton == null)
        {
            levelButton = GameObject.Find("Shoot").GetComponent<Button>();
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

    public void ReCheckTilemaps()
    {
        bool allCorrect = true;

        // Re-check every tilemap in the level
        foreach (var tilemapController in scalableTilemaps)
        {
            if (!tilemapController.IsCorrectTilemapActive())
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            if (!levelComplete)
            {
                levelComplete = true;
                Debug.Log("All correct tilemaps are active, awarding badge.");
                DisableBlockingObject();
            }
        }
        else
        {
            if (levelComplete)
            {
                levelComplete = false;
                Debug.Log("At least one tilemap has been changed away from correct. Removing badge.");
                EnableBlockingObject();
            }
        }
    }

    private void EnableBlockingObject()
    {
        if (blockingObject != null)
        {
            blockingObject.gameObject.SetActive(true); 
        }
        if (levelButton != null)
        {
            levelButton.interactable = false;
        }
    }


}
