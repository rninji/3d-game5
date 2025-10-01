using UnityEngine;
using UnityEngine.AI;
using static Constants;

public class EnemyStatePatrol : EnemyState, ICharacterState
{
    private float _waitTime = 0f;
    
    public EnemyStatePatrol(EnemyController enemyController, Animator animator, NavMeshAgent navMeshAgent) : base(enemyController, animator, navMeshAgent)
    {
    }

    public void Enter()
    {
        _waitTime = 0f;
        _navMeshAgent.isStopped = false;
        _animator.SetBool(EnemyAnimPramPatrol, true);
    }

    public void Update()
    {
        // Patrol > Chase 상태로 전환
        if (_waitTime > _enemyController.ChaseWaitTime)
        {
            var detectionTargetTransform = _enemyController.DetectionTargetInCircle();
            if (detectionTargetTransform)
            {
                _navMeshAgent.SetDestination(detectionTargetTransform.position);
                _enemyController.SetState(EEnemyState.Chase);
            }

            _waitTime = 0f;
        }

        // Patrol > Idle 상태로 전환
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _enemyController.SetState(EEnemyState.Idle);
        }

        // _waitTime 증가
        _waitTime += Time.deltaTime;
    }

    public void Exit()
    {
        _animator.SetBool(EnemyAnimPramPatrol, false);
    }
}