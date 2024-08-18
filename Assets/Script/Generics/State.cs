using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject, IObjectState
{
    public abstract void EnterState(ScalableObjectController obj);
    public abstract void UpdateState(ScalableObjectController obj);
}

