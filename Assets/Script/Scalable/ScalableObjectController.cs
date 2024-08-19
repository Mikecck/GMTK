using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScalableObjectController : MonoBehaviour
{
    [SerializeField]
    private GameObject correctTilemap; // The target Tilemap to identify success

    private List<GameObject> controllableChildren = new List<GameObject>();
    private Collider2D objectCollider;
    public bool isSelected = false;
    private int currentChildIndex = 0; // Index to track the currently active controllable child
    private bool isCorrectTilemapActive = false;

    private void Awake()
    {
        objectCollider = GetComponent<Collider2D>();
        GetControllableChildren();
    }

    private void GetControllableChildren()
    {
        controllableChildren.Clear();

        foreach (Transform child in transform)
        {
            if (child.GetComponent<TilemapRenderer>() != null)
            {
                controllableChildren.Add(child.gameObject);
                SetTilemapRendererEnabled(child.gameObject, false);
            }
        }
        if (controllableChildren.Count > 0)
        {
            SetTilemapRendererEnabled(controllableChildren[0], true);
        }
    }


    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckForSelection();
        }

        if (isSelected)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                CycleChildren(-1); // Cycle left
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                CycleChildren(1); // Cycle right
            }
        }
    }

    private void CycleChildren(int direction)
    {
        if (controllableChildren.Count == 0) return;

        // Disable the current child's TilemapRenderer and its background
        SetTilemapRendererEnabled(controllableChildren[currentChildIndex], false);

        // Adjust the index using modulo for wrapping
        currentChildIndex = (currentChildIndex + direction + controllableChildren.Count) % controllableChildren.Count;

        // Enable the new child's TilemapRenderer and its background
        SetTilemapRendererEnabled(controllableChildren[currentChildIndex], true);

        // Check if the newly enabled child's TilemapRenderer is the correct one
        if (controllableChildren[currentChildIndex] == correctTilemap)
        {
            if (!isCorrectTilemapActive)
            {
                isCorrectTilemapActive = true;
                GameManager.Instance.NotifyTilemapCorrect(this); // Notify that the correct tilemap is selected
            }
        }
        else
        {
            isCorrectTilemapActive = false;
        }
    }

    // Helper method to enable/disable the TilemapRenderer of a GameObject and its children
    private void SetTilemapRendererEnabled(GameObject obj, bool enabled)
    {
        var renderer = obj.GetComponent<TilemapRenderer>();
        if (renderer != null)
        {
            renderer.enabled = enabled;
        }
        // Also enable/disable the renderer for any child (assumed to be the background tilemap)
        foreach (Transform child in obj.transform)
        {
            var childRenderer = child.GetComponent<TilemapRenderer>();
            if (childRenderer != null)
            {
                childRenderer.enabled = enabled;
            }
        }
    }

    private void CheckForSelection()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider == objectCollider)
        {
            if (!isSelected)
            {
                GameManager.Instance.SelectObject(this); // Select this object
            }
        }
        else if (isSelected)
        {
            GameManager.Instance.DeselectCurrentObject(); // Deselect if clicking outside
        }
    }

    public bool IsCorrectTilemapActive()
    {
        return isCorrectTilemapActive;
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        // Additional visual feedback for selection can be added here, such as highlighting
    }
}
