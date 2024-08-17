using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NormalState : IObjectState
{
    public void EnterState(ScalableObjectController obj)
    {
        // Logic for entering small state
        obj.ChangeColor(Color.blue);
    }

    public void UpdateState(ScalableObjectController obj)
    {
        // Additional logic if needed
    }
}