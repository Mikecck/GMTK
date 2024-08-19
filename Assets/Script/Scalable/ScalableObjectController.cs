using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScalableObjectController : MonoBehaviour
{
    [SerializeField]
    private GameObject correctTilemap; // The target Tilemap to identify success

    private List<TilemapRenderer> tilemapRenderers = new List<TilemapRenderer>();
    private Collider2D objectCollider;
    public bool isSelected = false;
    private int currentTilemapIndex = 0; // Index to track the currently active TilemapRenderer
    private bool isCorrectTilemapActive = false;

    private void Awake()
    {
        objectCollider = GetComponent<Collider2D>();
        GetChildTilemapRenderers();
    }

    private void GetChildTilemapRenderers()
    {
        foreach (Transform child in transform)
        {
            TilemapRenderer tmRenderer = child.GetComponent<TilemapRenderer>();
            if (tmRenderer != null)
            {
                tilemapRenderers.Add(tmRenderer);
                tmRenderer.enabled = false; // Start with all TilemapRenderers disabled
            }
        }

        // Enable the first TilemapRenderer if any exist
        if (tilemapRenderers.Count > 0)
        {
            tilemapRenderers[0].enabled = true;
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
                CycleTilemapRenderers(-1); // Cycle left
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                CycleTilemapRenderers(1); // Cycle right
            }
        }
    }

    private void CycleTilemapRenderers(int direction)
    {
        if (tilemapRenderers.Count == 0) return;

        // Disable the current TilemapRenderer
        tilemapRenderers[currentTilemapIndex].enabled = false;

        // Adjust the index using modulo for wrapping
        currentTilemapIndex = (currentTilemapIndex + direction + tilemapRenderers.Count) % tilemapRenderers.Count;

        // Enable the new TilemapRenderer
        tilemapRenderers[currentTilemapIndex].enabled = true;

        // Check if the newly enabled TilemapRenderer is the correct one
        if (tilemapRenderers[currentTilemapIndex].gameObject == correctTilemap)
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
