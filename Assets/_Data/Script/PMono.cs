using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMono : MonoBehaviour
{

    protected virtual void Start()
    {
        this.Init();
    }

    public virtual void Init()
    {

    }

    protected virtual void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {

    }

    protected virtual void Awake()
    {
        this.LoadComponents();
    }

    public virtual void SetActive(bool status)
    {
        gameObject.SetActive(status);
    }
}
