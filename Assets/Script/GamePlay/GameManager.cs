using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    private ScalableObjectController currentSelectedObject;
    private List<ScalableObjectController> scalableObjects = new List<ScalableObjectController>();

    public void RegisterScalableObject(ScalableObjectController obj)
    {
        if (!scalableObjects.Contains(obj))
        {
            scalableObjects.Add(obj);
        }
    }

    public void UnregisterScalableObject(ScalableObjectController obj)
    {
        if (scalableObjects.Contains(obj))
        {
            scalableObjects.Remove(obj);
        }
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

    public void NotifySuccess(GameObject obj)
    {
        Debug.Log("Correct sprite enabled: " + obj.name);
        CheckForLevelCompletion();
    }

    private void CheckForLevelCompletion()
    {
        // Check if all scalable objects have their correct sprite active
        foreach (var scalableObject in scalableObjects)
        {
            if (!scalableObject.IsCorrectSpriteActive())
            {
                return; // If any object does not have the correct sprite, exit early
            }
        }

        // All correct sprites are active, proceed to the next level
        Debug.Log("All correct sprites are active, proceeding to the next level.");
        LevelManager.Instance.LoadNextLevel();
    }
}
