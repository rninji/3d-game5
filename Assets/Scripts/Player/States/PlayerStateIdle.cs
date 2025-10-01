using UnityEngine;
using UnityEngine.InputSystem;
using static Constants;

public class PlayerStateIdle : PlayerState, ICharacterState
{
    public PlayerStateIdle(PlayerController playerController, Animator animator, PlayerInput playerInput) 
        : base(playerController, animator, playerInput) { }

    public void Enter()
    {
        // 애니메이션 실행
        _animator.SetBool(PlayerAnimPramIdle, true);
        
        // PlayerInput에 대한 액션 할당
        _playerInput.actions["Fire"].performed += Attack;
        _playerInput.actions["Jump"].performed += Jump;
    }

    public void Update()
    {
        if (_playerInput.actions["Move"].IsPressed())
        {
            _playerController.SetState(EPlayerState.Move);
        }
    }

    public void Exit()
    {
        // 애니메이션 중단
        _animator.SetBool(PlayerAnimPramIdle, false);
        
        // PlayerInput에 대한 액션 해제
        _playerInput.actions["Fire"].performed -= Attack;
        _playerInput.actions["Jump"].performed -= Jump;
    }
}