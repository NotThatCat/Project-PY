using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // 5 Enemies variables
    [SerializeField] private float _maxHP;
    [SerializeField] private float _currentHP;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private bool _canMove;
    [SerializeField] private float _str;


    // 5 Enemies methods

    public virtual void TakeDamage(float damage)
    {
        this._currentHP -= damage;
        if (this._currentHP < 0) {  this._currentHP = 0; }
    }

    public virtual float GetHP()
    {
        return this._currentHP;
    }

    public virtual float GetSpeed()
    {
        return this._currentSpeed;
    }

    public virtual bool CanMove()
    {
        return this._canMove;
    }

    public virtual float GetDamage()
    {
        return this._str;
    }
}
