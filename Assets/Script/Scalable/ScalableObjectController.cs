using UnityEngine;
using System.Collections.Generic;

public class ScalableObjectController : MonoBehaviour
{
    [SerializeField]
    private GameObject correctSprite; // This is the target sprite to identify success

    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    private Collider2D objectCollider;
    public bool isSelected = false;
    private int currentSpriteIndex = 0; // Index to track the currently active sprite

    private void Awake()
    {
        objectCollider = GetComponent<Collider2D>(); // Assuming collider is attached for raycasting
        GetChildSpriteRenderers();
    }

    private void Update()
    {
        HandleInput();
    }

    private void GetChildSpriteRenderers()
    {
        foreach (Transform child in transform)
        {
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                spriteRenderers.Add(sr);
                sr.enabled = false;
            }
        }

        if (spriteRenderers.Count > 0)
        {
            spriteRenderers[0].enabled = true;
        }
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
                CycleSprites(-1); // Cycle left
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                CycleSprites(1); // Cycle right
            }
        }
    }

    private void CycleSprites(int direction)
    {
        if (spriteRenderers.Count == 0) return;

        // Disable the current sprite
        spriteRenderers[currentSpriteIndex].enabled = false;

        // Adjust the index using modulo for wrapping
        currentSpriteIndex = (currentSpriteIndex + direction + spriteRenderers.Count) % spriteRenderers.Count;

        // Enable the new sprite
        spriteRenderers[currentSpriteIndex].enabled = true;

        // Check if the currently enabled sprite is the correct one
        if (spriteRenderers[currentSpriteIndex].gameObject == correctSprite)
        {
            GameManager.Instance.NotifySuccess(gameObject);
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

    public void SetSelected(bool selected)
    {
        isSelected = selected; // Set selection state based on GameManager's decision
        // Additional visual feedback for selection can be added here, such as highlighting
    }
}
