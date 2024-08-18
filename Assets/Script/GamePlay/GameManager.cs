using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float timeLimit = 10f;
    private float timeRemaining;
    private bool levelInProgress = false;
    private HashSet<int> collectedBadges = new HashSet<int>(); // Collected badges
    public ScalableObjectController scalableObject; // Assign in Inspector

    public float targetScaleMultiplier = 2f; // The target scale multiplier for winning

    // Events to notify the UI
    public event System.Action OnLevelStart;

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
        if (levelInProgress)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                EndLevel(false);
            }
            else if (CheckWinningCondition() & timeRemaining >= 0)
            {
                EndLevel(true);
            }
        }
    }

    public void StartLevel()
    {
        timeRemaining = timeLimit;
        levelInProgress = true;
        OnLevelStart?.Invoke();
    }

    private bool CheckWinningCondition()
    {
        return Mathf.Approximately(scalableObject.GetCurrentScaleMultiplier(), targetScaleMultiplier);
    }

    public void EndLevel(bool success)
    {
        if (success)
        {
            GrantBadge(0); // Badge index can be extended
            Debug.Log("Level Completed!");
        }
        else
        {
            Debug.Log("Level Failed!");
        }

        levelInProgress = false;
        // You can reset the level or move to the next one here
    }

    private void GrantBadge(int levelIndex)
    {
        collectedBadges.Add(levelIndex);
        Debug.Log("Badge granted for level: " + levelIndex);
    }
}
