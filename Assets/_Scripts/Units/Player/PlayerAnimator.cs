using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator _anim;
    private Rigidbody _rb;
    private PlayerHealth _playerHealth;
    private bool _idleAnimationChangeFlag = false;
    private int _selectedIdleAnimation;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerHealth = GetComponent<PlayerHealth>();
        _anim = GetComponent<Animator>();
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
        if (_playerHealth.IsPlayerDead())//Death
        {
            return Death;
        }else if(_rb.velocity == Vector3.zero) //Idle
        {
            bool randomBool = (UnityEngine.Random.Range(0, 2) == 0);
            if (_idleAnimationChangeFlag)
            {
                _idleAnimationChangeFlag = false;
                if (randomBool) { _selectedIdleAnimation = Idle1; return Idle1; } else { _selectedIdleAnimation = Idle2; return Idle2; }
            }
            else
            {
                return _selectedIdleAnimation;
            }
        }
        else
        {
            _idleAnimationChangeFlag = true;
            return Run;
        }
    }

    #region Cached Properties

    private int _currentState;

    private static readonly int Idle1 = Animator.StringToHash("Idle1");
    private static readonly int Idle2 = Animator.StringToHash("Idle2");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int DeathRecovery = Animator.StringToHash("DeathRecovery");
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int Run = Animator.StringToHash("Run");

    #endregion
}