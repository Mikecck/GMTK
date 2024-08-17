using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scalable Object State Interface
public interface IObjectState
{
    void EnterState(ScalableObjectController obj);
    void UpdateState(ScalableObjectController obj);
}