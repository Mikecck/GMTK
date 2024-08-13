using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Vector3 originalPosition;
    public Transform[] snapPoints;
    public float snapRange = 1f;

    void Start()
    {
        originalPosition = transform.position;
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        Transform snapTarget = GetSnapTarget();
        if (snapTarget != null)
        {
            transform.position = snapTarget.position;
        }
        else
        {
            transform.position = originalPosition;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    private Transform GetSnapTarget()
    {
        foreach (var snapPoint in snapPoints)
        {
            if (Vector3.Distance(transform.position, snapPoint.position) <= snapRange)
            {
                return snapPoint;
            }
        }
        return null;
    }
}