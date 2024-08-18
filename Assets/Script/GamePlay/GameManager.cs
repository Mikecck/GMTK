using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int currentThemeIndex = 0;
    [SerializeField] private int currentLevelIndex = 0;
    [SerializeField] private float timeLimit = 10f;

    private float timeRemaining;
    private bool levelInProgress = false;
    private HashSet<int> collectedBadges = new HashSet<int>(); // Collected badges

    // Events to notify the UI
    public event System.Action OnLevelStart;
    public event System.Action OnLevelEnd;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        StartLevel();
    }

    private void Update()
    {
        // Update the time remaining
        if (levelInProgress)
        {
            timeRemaining -= Time.deltaTime;
            // Debug.Log("Time Remaining: " + timeRemaining);
            if (timeRemaining <= 0)
            {
                EndLevel(true);
            }
        }
    }

    // Start the level
    public void StartLevel()
    {
        timeRemaining = timeLimit;
        levelInProgress = true;
        OnLevelStart?.Invoke();

        // Initialize the current level logic here
        LevelManager.Instance.LoadCurrentLevel(currentThemeIndex, currentLevelIndex);
    }

    public void OnLevelConditionMet()
    {
        EndLevel(true);  // End the level successfully
    }

    // End the level
    public void EndLevel(bool success)
    {
        levelInProgress = false;
        OnLevelEnd?.Invoke();

        if (success && LevelManager.Instance.CheckWinningCondition(currentThemeIndex, currentLevelIndex))
        {
            GrantBadge(currentLevelIndex);
            currentLevelIndex++;
            if (currentLevelIndex >= LevelManager.Instance.GetLevelCount(currentThemeIndex))
            {
                currentLevelIndex = 0;
                if (CheckAllBadgesCollected())
                {
                    UnlockNextTheme();
                }
            }
        }
        StartLevel();
    }

    private void GrantBadge(int levelIndex)
    {
        collectedBadges.Add(levelIndex);
        Debug.Log("Badge granted for level: " + levelIndex);
    }

    private bool CheckAllBadgesCollected()
    {
        return collectedBadges.Count >= LevelManager.Instance.GetLevelCount(currentThemeIndex);
    }

    private void UnlockNextTheme()
    {
        Debug.Log("All badges collected. Unlocking next theme.");
        currentThemeIndex++;
        collectedBadges.Clear();
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    public bool IsLevelInProgress()
    {
        return levelInProgress;
    }
}
