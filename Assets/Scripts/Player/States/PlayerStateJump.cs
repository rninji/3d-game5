using UnityEngine;
using UnityEngine.InputSystem;
using static Constants;
using static CharacterUtility;

public class PlayerStateJump : PlayerState, ICharacterState
{
    public PlayerStateJump(PlayerController playerController, Animator animator, PlayerInput playerInput) : base(playerController, animator, playerInput)
    {
    }

    public void Enter()
    {
        // 애니메이션 실행
        _animator.SetTrigger(PlayerAnimPramJump);
    }

    public void Update()
    {
        // 캐릭터 방향 설정
        var moveVector = _playerInput.actions["Move"].ReadValue<Vector2>();
        if (moveVector != Vector2.zero)
        {
            Rotate(moveVector.x, moveVector.y);
        }
        
        // Ground Distance 업데이트
        var playerPosition = _playerController.transform.position;
        var distance = GetDistanceToGround(playerPosition, GroundLayerMask, 3f);
        _animator.SetFloat(PlayerAnimPramGroundDistance, distance);
        
        Debug.DrawRay(playerPosition, Vector3.down * 3f, Color.red);
    }

    public void Exit()
    {
        
    }
}