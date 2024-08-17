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
                ScaleObject(2f);
                ChangeState(SmallState);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                ScaleObject(4f);
                ChangeState(LargeState);
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

    void ScaleObject(float scaleFactor)
    {
        transform.localScale = initialScale * scaleFactor;
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