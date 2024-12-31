using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : PMono
{
    [SerializeField] protected Transform pos;
    [SerializeField] protected Transform wallBottom;
    [SerializeField] protected Transform wallRight;
    [SerializeField] protected int _width = 20;
    [SerializeField] protected int _height = 20;

    protected override void LoadComponents()
    {
        //this.CreateMap();
    }

    protected virtual void Start()
    {
        //this.CreateMap();
    }

    protected virtual void CreateMap()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Transform newWall = Instantiate(this.GetWall(), transform.position, transform.rotation, transform);
                //Transform newWall = Instantiate(pos, transform.position, transform.rotation, transform);
                newWall.transform.SetPositionAndRotation(this.GetPos(i +1, j+1), transform.rotation);
            }
        }
    }

    protected virtual Transform GetWall()
    {
        var isBottom = Random.Range(0, 2) == 0 ? true : false;
        if (isBottom) { return wallBottom; }
        return wallRight;
    }

    protected virtual Vector3 GetPos(int width, int height)
    {
        Vector3 pos = new Vector3();
        pos.x = 2.5f - ((float)width - ((float)_width / 2f)) * 5f;
        pos.y = 0.5f;
        pos.z = 2.5f - ((float)height - ((float)_height / 2f)) * 5f;
        //Debug.Log(width + " " + height + " " + pos);
        return pos;
    }
}
