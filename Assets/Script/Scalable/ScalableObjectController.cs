using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScalableObjectController : MonoBehaviour
{
    private Vector3 initialScale;
    private bool isSelected = false;
    private bool isScaling = false;
    public State currentState;

    public float scalingDuration = 0.5f;
    public AnimationCurve scalingCurve;

    public State SmallState;
    public State NormalState;
    public State LargeState;

    void Start()
    {
        initialScale = transform.localScale; // Add the initial scale
        if (currentState != null)
            currentState.EnterState(this);
    }

    void Update()
    {
        if (isSelected)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ScaleDown();
                Debug.Log("Input: Scale Down");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                ScaleUp();
                Debug.Log("Input: Scale Up");
            }
        }
    }

    void OnMouseDown()
    {
        isSelected = true;
    }

    void OnMouseUp()
    {
        isSelected = false;
    }

    void ScaleDown()
    {
        if (isScaling) return;

        if (currentState == NormalState)
        {
            StartCoroutine(AnimateScale(initialScale * 0.5f));
            ChangeState(SmallState);
            Debug.Log("Scale down to Small State");
        }
        else if (currentState == LargeState)
        {
            StartCoroutine(AnimateScale(initialScale));
            ChangeState(NormalState);
            Debug.Log("Scale down to Normal State");
        }
        // If already in SmallState, do nothing
    }

    void ScaleUp()
    {
        if (isScaling) return;

        if (currentState == NormalState)
        {
            StartCoroutine(AnimateScale(initialScale * 2f));
            ChangeState(LargeState);
            Debug.Log("Scale up to Large State");
        }
        else if (currentState == SmallState)
        {
            StartCoroutine(AnimateScale(initialScale));
            ChangeState(NormalState);
            Debug.Log("Scale up to Normal State");
        }
        // If already in LargeState, do nothing
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

    public void ChangeState(State newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    /* This is for Debug Only
    public void ChangeColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
    */
    public State GetCurrentState()
    {
        return currentState;
    }

}