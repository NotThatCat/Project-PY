using UnityEngine;
using UnityEngine.AI;

public class EnemyMoving : EnemyAbstract
{
    [Header("Path Info")]
    [SerializeField] protected PathMoving path;
    [SerializeField] protected int currentPointIndex = 0;

    [Header("Distance Settings")]
    [SerializeField] protected float pointDistanceLimit = 1f;

    [Header("Movement Flags")]
    [SerializeField] protected bool canMove = true;
    [SerializeField] protected bool isFinish = false;
    [SerializeField] protected bool isMoving = false;

    private float updateInterval = 0.2f;
    private float nextUpdateTime = 0f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPathMoving();
    }

    protected virtual void LoadPathMoving()
    {
        if (this.path == null)
        {
            PathMoving defaultPath = GameObject.Find("PathMoving0")?.GetComponent<PathMoving>();
            if (defaultPath != null) this.path = defaultPath;
        }
    }

    public virtual void SetPath(PathMoving newPath)
    {
        this.path = newPath;
    }

    protected virtual void OnEnable()
    {
        this.currentPointIndex = 0;
        this.isFinish = false;
        this.canMove = true;

        this.ctrl.Agent.isStopped = false;
    }

    void Update()
    {
        if (Time.time >= nextUpdateTime)
        {
            nextUpdateTime = Time.time + updateInterval;
            UpdateMovement();
        }

        UpdateMovingStatus();
    }

    protected virtual void UpdateMovement()
    {
        if (this.isFinish || !this.canMove || IsDead())
        {
            this.ctrl.Agent.isStopped = true;
            return;
        }

        if (!this.ctrl.Agent.pathPending)
        {
            if (this.ctrl.Agent.remainingDistance <= this.pointDistanceLimit)
            {
                this.currentPointIndex++;
                if (this.path != null && this.currentPointIndex >= this.path.Points.Count)
                {
                    this.OnFinish();
                    return;
                }
            }
        }

        if (this.path != null && this.currentPointIndex < this.path.Points.Count)
        {
            Transform nextPoint = this.path.Points[this.currentPointIndex].transform;
            this.ctrl.Agent.SetDestination(nextPoint.position);
        }
    }

    protected virtual bool IsDead()
    {
        return this.ctrl.EnemyDamageReceiver.IsDead();
    }

    protected virtual void UpdateMovingStatus()
    {
        if (this.isFinish) return;
        bool currentMoving = !this.ctrl.Agent.isStopped;
        if (currentMoving != this.isMoving)
        {
            this.isMoving = currentMoving;
        }
    }

    protected virtual void OnFinish()
    {
        this.isFinish = true;
        this.ctrl.Agent.isStopped = true;
        PlayerCtrl.Instance.PlayerDamageReceiver.Receive(1, null);
        this.ctrl.Despawn.DoDespawn();
    }
}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class EnemyMoving : EnemyAbstract
//{
//    [SerializeField] protected PathMoving path;
//    [SerializeField] protected int currentPointIndex = 0;
//    [SerializeField] protected Point currentPoint;
//    [SerializeField] protected float pointDistance = Mathf.Infinity;
//    [SerializeField] protected float pointDistanceLimit = 1f;
//    [SerializeField] protected bool canMove = true;
//    [SerializeField] protected bool isFinish = false;
//    [SerializeField] protected bool isMoving = false;

//    void FixedUpdate()
//    {
//        this.Moving();
//    }

//    protected override void LoadComponents()
//    {
//        base.LoadComponents();
//        this.LoadPathMoving();
//    }

//    protected virtual void LoadPathMoving()
//    {
//        if (path != null) return;
//        path = GameObject.Find("PathMoving0").GetComponent<PathMoving>();
//    }

//    public virtual void SetPath(PathMoving path)
//    {
//        this.path = path;
//    }

//    protected virtual void Moving()
//    {
//        this.LoadMovingStatus();
//        if (this.isFinish || !this.canMove || this.IsDead())
//        {
//            this.ctrl.Agent.isStopped = true;
//            return;
//        }

//        this.GetNextPoint();
//        this.ctrl.Agent.SetDestination(this.currentPoint.transform.position);
//    }

//    protected virtual bool IsDead()
//    {
//        return this.ctrl.EnemyDamageReceiver.IsDead();
//    }

//    protected virtual void GetNextPoint()
//    {
//        currentPoint = path.GetPoint(currentPointIndex);
//        pointDistance = Vector3.Distance(currentPoint.transform.position, transform.position);
//        if (pointDistance < pointDistanceLimit) currentPointIndex++;
//        if (currentPointIndex > path.Points.Count - 1) isFinish = true;
//    }

//    protected virtual void LoadMovingStatus()
//    {
//        isMoving = !this.ctrl.Agent.isStopped;
//        //this.ctrl.Animator.SetBool("isMoving", isMoving);
//    }

//    protected virtual void OnEnable()
//    {
//        this.currentPointIndex = 0;
//    }
//}
