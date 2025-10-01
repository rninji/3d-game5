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

    // ---------------------------------------------
    // Enemy 상태
    public enum EEnemyState
    {
        None, Idle, Patrol, Chase, Attack, Hit, Dead
    }
    
    // ---------------------------------------------
    // Player Animator 파라미터
    public static readonly int EnemyAnimPramIdle = Animator.StringToHash("idle");
    public static readonly int EnemyAnimPramPatrol = Animator.StringToHash("patrol");
    public static readonly int EnemyAnimPramChase = Animator.StringToHash("chase");
    public static readonly int EnemyAnimPramAttack = Animator.StringToHash("attack");
    public static readonly int EnemyAnimPramHit = Animator.StringToHash("hit");
    public static readonly int EnemyAnimPramDead = Animator.StringToHash("dead");
    public static readonly int EnemyAnimPramMoveSpeed = Animator.StringToHash("move_speed");

}