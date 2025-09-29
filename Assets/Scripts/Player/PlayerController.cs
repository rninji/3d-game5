using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Constants;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private PlayerInput _playerInput;
    public EPlayerState State { get; private set; }

    private Dictionary<EPlayerState, IPlayerState> _states;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();

        var playerStateIdle = new PlayerStateIdle(this, _animator);

        _states = new Dictionary<EPlayerState, IPlayerState>()
        {
            { EPlayerState.Idle , playerStateIdle},
        };
    }

    private void Update()
    {
        if (State != EPlayerState.None)
            _states[State].Update();
    }

    // 새로운 상태 할당
    public void SetState(EPlayerState state)
    {
        if (State == state) return;
        if(State != EPlayerState.None) _states[State].Exit();
        State = state;
        if(State != EPlayerState.None) _states[State].Enter();
    }
}
