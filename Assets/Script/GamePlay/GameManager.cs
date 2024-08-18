using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private ScalableObjectController currentSelectedObject;

    [SerializeField]
    private List<ScalableObjectController> scalableObjects; // List of scalable objects in the level

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndAssignScalableObjects();
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
        // Check the status of all objects
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
            Debug.Log("All correct sprites are active, proceeding to the next level.");
            LevelManager.Instance.LoadNextLevel();  // Load the next level correctly interfacing with LevelManager
        }
        else
        {
            Debug.Log("Not all correct sprites are active yet.");
        }
    }

}
