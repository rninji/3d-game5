using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static Constants;

public class PlayerStateHit : PlayerState, ICharacterState
{
    public PlayerStateHit(PlayerController playerController, Animator animator, PlayerInput playerInput) : base(playerController, animator, playerInput)
    {
    }

    public void Enter()
    {
        _animator.SetTrigger(PlayerAnimPramHit);
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        
    }
}