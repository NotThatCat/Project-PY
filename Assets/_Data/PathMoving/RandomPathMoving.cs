using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RandomPathMoving : PathMoving
{
    protected override void LoadPoints()
    {
        if (this._points.Count > 0)
        {
            _points = new List<Point>();
        };
        Point point;
        foreach (Transform child in transform)
        {
            point = child.GetComponent<Point>();
            if (point == null) continue;
            this._points.Add(point);
        }

        this.ShuffleList<Point>(this._points);
    }

    protected virtual void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);

            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
