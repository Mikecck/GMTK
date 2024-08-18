using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableObjectController : MonoBehaviour
{
    private Vector3 initialScale;
    private bool isSelected = false;
    private bool isScaling = false;

    [SerializeField] private float scalingDuration = 0.5f;
    [SerializeField] private AnimationCurve scalingCurve;

    [SerializeField] private float smallestScale = 0.5f; 
    [SerializeField] private float largestScale = 2f;
    [SerializeField] private Vector3 targetScale;

    void Start()
    {
        initialScale = transform.localScale;
        targetScale = initialScale;
    }

    void Update()
    {
        if (isSelected)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ScaleDown();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                ScaleUp();
            }
        }
    }

    void OnMouseDown()
    {
        isSelected = !isSelected; // Toggle selection
        Debug.Log(isSelected ? "Object selected" : "Object deselected");
    }

    void ScaleDown()
    {
        if (isScaling) return;

        Vector3 nextScale = transform.localScale * 0.5f;

        if (nextScale.x >= initialScale.x * smallestScale) // Ensure it doesn't go below the smallest scale
        {
            targetScale = nextScale;
        }
        else
        {
            targetScale = initialScale * smallestScale;
        }

        StartCoroutine(AnimateScale(targetScale));
    }

    void ScaleUp()
    {
        if (isScaling) return;

        Vector3 nextScale = transform.localScale * 2f;

        if (nextScale.x <= initialScale.x * largestScale)
        {
            targetScale = nextScale;
        }
        else
        {
            targetScale = initialScale * largestScale; // Set to largest scale if above limit
        }

        StartCoroutine(AnimateScale(targetScale));
    }

    private IEnumerator AnimateScale(Vector3 targetScale)
    {
        isScaling = true;
        Vector3 startScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < scalingDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / scalingDuration;
            float curveValue = scalingCurve.Evaluate(t);
            transform.localScale = Vector3.Lerp(startScale, targetScale, curveValue);
            yield return null;
        }

        transform.localScale = targetScale;
        isScaling = false;
    }

    public float GetCurrentScaleMultiplier()
    {
        return transform.localScale.x / initialScale.x;
    }
}
