using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScalableObjectController : MonoBehaviour
{
    private Vector3 initialScale;
    private bool isSelected = false;
    private IObjectState currentState;

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
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                ScaleUp();
            }
        }
    }

    void ScaleDown()
    {
        if (currentState == NormalState)
        {
            transform.localScale = initialScale * 0.5f;
            ChangeState(SmallState);
        }
        else if (currentState == LargeState)
        {
            transform.localScale = initialScale;
            ChangeState(NormalState);
        }
        // If already in SmallState, do nothing
    }

    void ScaleUp()
    {
        if (currentState == NormalState)
        {
            transform.localScale = initialScale * 2f;
            ChangeState(LargeState);
        }
        else if (currentState == SmallState)
        {
            transform.localScale = initialScale;
            ChangeState(NormalState);
        }
        // If already in LargeState, do nothing
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