using UnityEngine;
using UnityEngine.AI;
using static Constants;

public class EnemyStateAttack : EnemyState, ICharacterState
{
    public EnemyStateAttack(EnemyController enemyController, Animator animator, NavMeshAgent navMeshAgent) : base(enemyController, animator, navMeshAgent)
    {
    }

    public void Enter()
    {
        _animator.SetTrigger(EnemyAnimPramAttack);
    }

    public void Update()
    {
            
    }

    public void Exit()
    {
            
    }
}