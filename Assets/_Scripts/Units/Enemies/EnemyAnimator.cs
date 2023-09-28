using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Enemy _enemy;
    private Animator _anim;
    private Rigidbody _rb;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var state = GetState();

        if (state == _currentState) return;

        _anim.CrossFade(state, 0, 0);
        _currentState = state;
    }

    private int GetState()
    {
        if (_enemy.IsItDead())
        {
            return Death;
        }
        else if (_enemy.IsItAttacking())
        {
            return Attack;
        }
        else if(_rb.velocity == Vector3.zero)
        {
            return Idle;
        }
        else
        {
            return Run;
        }
    }

    #region Cached Properties

    private int _currentState;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Death = Animator.StringToHash("Death");

    #endregion
}
