using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScalableObjectController : MonoBehaviour
{
    private Vector3 initialScale;
    private bool isSelected = false;
    private IObjectState currentState;

    public float scalingDuration = 0.5f;
    public AnimationCurve scalingCurve;

    public IObjectState SmallState = new SmallerState();
    public IObjectState NormalState = new NormalState();
    public IObjectState LargeState = new LargerState();


    void Start()
    {
        initialScale = transform.localScale;
        currentState = NormalState;
        currentState.EnterState(this);
    }

    void Update()
    {
        if (isSelected)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ScaleDown();
                Debug.Log("Scale Down");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                ScaleUp();
                Debug.Log("Scale Up");
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
        if (currentState == NormalState)
        {
            StartCoroutine(AnimateScale(initialScale * 0.5f));
            ChangeState(SmallState);
        }
        else if (currentState == LargeState)
        {
            StartCoroutine(AnimateScale(initialScale));
            ChangeState(NormalState);
        }
        // If already in SmallState, do nothing
    }

    void ScaleUp()
    {
        if (currentState == NormalState)
        {
            StartCoroutine(AnimateScale(initialScale * 2f));
            ChangeState(LargeState);
        }
        else if (currentState == SmallState)
        {
            StartCoroutine(AnimateScale(initialScale));
            ChangeState(NormalState);
        }
        // If already in LargeState, do nothing
    }

    private IEnumerator AnimateScale(Vector3 targetScale)
    {
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
    }

    public void ChangeState(IObjectState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
    public void ChangeColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }

}