using UnityEngine;
using UnityEngine.Tilemaps;

public class Hoverover : MonoBehaviour
{
    [SerializeField] private ScalableObjectController targetController;

    private TilemapRenderer tilemapRenderer;
    private Material tilemapMaterial;
    private Color originalColor;
    private float targetAlpha;
    private float currentAlpha;
    private float hoverTransparency = 0.1f;
    private float normalTransparency = 1f;
    private float fadeSpeed = 2f;

    void Awake()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        if (tilemapRenderer != null)
        {
            tilemapMaterial = tilemapRenderer.material;
            originalColor = tilemapMaterial.color;
            currentAlpha = originalColor.a;
            targetAlpha = normalTransparency;
        }
    }

    void Update()
    {
        float alphaChange = fadeSpeed * Time.deltaTime * 0.4f;

        if (currentAlpha > targetAlpha)
        {
            currentAlpha -= alphaChange;
            if (currentAlpha < targetAlpha)
            {
                currentAlpha = targetAlpha;
            }
        }
        else if (currentAlpha < targetAlpha)
        {
            currentAlpha += alphaChange;
            if (currentAlpha > targetAlpha)
            {
                currentAlpha = targetAlpha;
            }
        }

        tilemapMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, currentAlpha);
    }

    void OnMouseEnter()
    {
        targetAlpha = hoverTransparency;
        if (targetController != null && !targetController.isSelected)
        {
            targetController.SetSelected(true);
            GameManager.Instance.SelectObject(targetController);
        }
    }

    void OnMouseExit()
    {
        targetAlpha = normalTransparency;
        if (targetController != null && targetController.isSelected)
        {
            targetController.SetSelected(false);
            GameManager.Instance.DeselectCurrentObject();
        }
    }
}
