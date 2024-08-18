using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private ScalableObjectController currentSelectedObject;

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
        // Here, you would check if all necessary conditions are met to proceed to the next level
        CheckForLevelCompletion();
    }

    private void CheckForLevelCompletion()
    {
        // Logic to determine if all correct sprites are active
        // and possibly calling LevelManager.Instance.GoToNextLevel();
    }
}
