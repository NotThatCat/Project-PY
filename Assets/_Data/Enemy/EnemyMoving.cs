using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoving : EnemyAbstract
{
    [SerializeField] protected PathMoving path;
    [SerializeField] protected int currentPointIndex = 0;
    [SerializeField] protected Point currentPoint;
    [SerializeField] protected float pointDistance = Mathf.Infinity;
    [SerializeField] protected float pointDistanceLimit = 1f;
    [SerializeField] protected bool canMove = true;
    [SerializeField] protected bool isFinish = false;
    [SerializeField] protected bool isMoving = false;

    void FixedUpdate()
    {
        this.Moving();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPathMoving();
    }

    protected virtual void LoadPathMoving()
    {
        if (path != null) return;
        path = GameObject.Find("PathMoving0").GetComponent<PathMoving>();
    }

    public virtual void SetPath(PathMoving path)
    {
        this.path = path;
    }

    protected virtual void Moving()
    {
        this.LoadMovingStatus();
        if (this.isFinish || !this.canMove || this.IsDead())
        {
            this.ctrl.Agent.isStopped = true;
            return;
        }

        this.GetNextPoint();
        this.ctrl.Agent.SetDestination(this.currentPoint.transform.position);
    }

    protected virtual bool IsDead()
    {
        return this.ctrl.EnemyDamageReceiver.IsDead();
    }

    protected virtual void GetNextPoint()
    {
        currentPoint = path.GetPoint(currentPointIndex);
        pointDistance = Vector3.Distance(currentPoint.transform.position, transform.position);
        if (pointDistance < pointDistanceLimit) currentPointIndex++;
        if (currentPointIndex > path.Points.Count - 1) isFinish = true;
    }

    protected virtual void LoadMovingStatus()
    {
        isMoving = !this.ctrl.Agent.isStopped;
        this.ctrl.Animator.SetBool("isMoving", isMoving);
    }

    protected virtual void OnEnable()
    {
        this.currentPointIndex = 0;
    }
}
