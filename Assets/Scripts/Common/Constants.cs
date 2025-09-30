using UnityEngine;

public class Constants
{
    public const float Gravity = -9.81f;
    
    // ---------------------------------------------
    // Layer Mask
    public static LayerMask GroundLayerMask => LayerMask.GetMask("Ground");
    
    // ---------------------------------------------
    // Player 상태
    public enum EPlayerState
    {
        None, Idle, Move, Jump, Attack, Hit, Dead
    }
    
    // ---------------------------------------------
    // Player Animator 파라미터
    public static readonly int PlayerAnimPramIdle = Animator.StringToHash("idle");
    public static readonly int PlayerAnimPramMove = Animator.StringToHash("move");
    public static readonly int PlayerAnimPramJump = Animator.StringToHash("jump");
    public static readonly int PlayerAnimPramAttack = Animator.StringToHash("attack");
    public static readonly int PlayerAnimPramMoveSpeed = Animator.StringToHash("move_speed");
    public static readonly int PlayerAnimPramGroundDistance = Animator.StringToHash("ground_distance");


}