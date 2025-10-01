using UnityEngine;
using UnityEngine.InputSystem;
using static Constants;

public class PlayerStateAttack : PlayerState, ICharacterState
{
    public PlayerStateAttack(PlayerController playerController, Animator animator, PlayerInput playerInput) : base(playerController, animator, playerInput)
    {
    }

    public void Enter()
    {
        _animator.SetTrigger(PlayerAnimPramAttack);
        _playerInput.actions["Fire"].performed += AttackTrigger;
    }

    public void Update()
    {
    }

    public void Exit()
    {
        _playerInput.actions["Fire"].performed -= AttackTrigger;
    }

    void AttackTrigger(InputAction.CallbackContext context)
    {
        _animator.SetTrigger(PlayerAnimPramAttack);
    }
}