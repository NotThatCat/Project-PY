using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathMoving : PMono
{
    [SerializeField] protected List<Point> _points = new();
    public List<Point> Points => _points;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoints();
    }

    protected virtual void LoadPoints()
    {
        if (this._points.Count > 0) return;
        Point point;
        foreach (Transform child in transform)
        {
            point = child.GetComponent<Point>();
            if (point == null) continue;
            this._points.Add(point);
        }
    }

    public virtual Point GetPoint(int pointIndex)
    {
        return this._points[pointIndex];
    }
}
