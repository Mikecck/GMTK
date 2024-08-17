using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int currentThemeIndex = 0;
    [SerializeField] private int currentLevelIndex = 0;
    [SerializeField] private float timeLimit = 10f;

    private float timeRemaining;
    private bool levelInProgress = false;


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
            Debug.Log("Time Remaining: " + timeRemaining);
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

    // End the level
    public void EndLevel(bool success)
    {
        levelInProgress = false;
        OnLevelEnd?.Invoke();

        if (success)
        {
            currentLevelIndex++;
            if (currentLevelIndex >= LevelManager.Instance.GetLevelCount(currentThemeIndex))
            {
                currentThemeIndex++;
                currentLevelIndex = 0;
                UnlockNextTheme();
            }
        }
        StartLevel();
    }

    private void UnlockNextTheme()
    {
        Debug.Log("Unlocking next theme");
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
