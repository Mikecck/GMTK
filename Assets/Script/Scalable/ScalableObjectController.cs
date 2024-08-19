using System.Collections.Generic;
using UnityEngine;

public class ScalableObjectController : MonoBehaviour
{
    [SerializeField]
    private GameObject correctSprite; // This is the target sprite to identify success

    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    private Collider2D objectCollider;
    public bool isSelected = false;
    private int currentSpriteIndex = 0; // Index to track the currently active sprite
    private bool isCorrectSpriteSelected = false;
    private void Awake()
    {
        objectCollider = GetComponent<Collider2D>();
        GetChildSpriteRenderers();
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

        if (spriteRenderers[currentSpriteIndex].gameObject == correctSprite)
        {
            if (!isCorrectSpriteSelected)
            {
                isCorrectSpriteSelected = true;
                GameManager.Instance.NotifySpriteCorrect(this);
            }
        }
        else
        {
            isCorrectSpriteSelected = false;
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

    public bool IsCorrectSpriteActive()
    {
        return isCorrectSpriteSelected;
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        // Additional visual feedback for selection can be added here, such as highlighting
    }
}
