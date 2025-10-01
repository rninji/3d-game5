using UnityEngine;
using UnityEngine.InputSystem;
using static Constants;

public class PlayerStateMove : PlayerState, ICharacterState
{
    private float _moveSpeed;
    
    public PlayerStateMove(PlayerController playerController, Animator animator, PlayerInput playerInput) 
        : base(playerController, animator, playerInput) { }


    public void Enter()
    {
        // 애니메이션 실행
        _animator.SetBool(PlayerAnimPramMove, true);
        
        // PlayerInput에 대한 액션 할당
        _playerInput.actions["Fire"].performed += Attack;
        _playerInput.actions["Jump"].performed += Jump;
        
        // moveSpeed 초기화
        _moveSpeed = 0f;
    }

    public void Update()
    {
        var moveVector = _playerInput.actions["Move"].ReadValue<Vector2>();
        if (moveVector != Vector2.zero)
        {
            Rotate(moveVector.x, moveVector.y);
        }
        else
        {
            _playerController.SetState(EPlayerState.Idle);
        }
        
        // 이동 스피드 설정
        var isRun = _playerInput.actions["Run"].IsPressed();
        if (isRun && _moveSpeed < 1f)
        {
            _moveSpeed += Time.deltaTime;
            _moveSpeed = Mathf.Clamp01(_moveSpeed);
        }
        else if (!isRun && _moveSpeed > 0f)
        {
            _moveSpeed -= Time.deltaTime * _playerController.breakForce;
            _moveSpeed = Mathf.Clamp01(_moveSpeed);
        }
        _animator.SetFloat(PlayerAnimPramMoveSpeed, _moveSpeed);
    }

    public void Exit()
    {
        // 애니메이션 중단
        _animator.SetBool(PlayerAnimPramMove, false);
        
        // PlayerInput에 대한 액션 해제
        _playerInput.actions["Fire"].performed -= Attack;
        _playerInput.actions["Jump"].performed -= Jump;
    }
}