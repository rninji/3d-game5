using UnityEngine;
using UnityEngine.AI;
using static Constants;

public class EnemyStateIdle : EnemyState, ICharacterState
{
    private float _waitTime;

    public EnemyStateIdle(EnemyController enemyController, Animator animator, NavMeshAgent navMeshAgent) 
        : base(enemyController, animator, navMeshAgent) { }

    public void Enter()
    {
        _waitTime = 0f;
        _navMeshAgent.isStopped = true;
        _animator.SetBool(EnemyAnimPramIdle, true);
    }

    public void Update()
    {
        // Idle > Chase 상태로 전환
        var detectionTargetTransform = _enemyController.DetectionTargetInCircle();
        if (detectionTargetTransform && _waitTime > _enemyController.ChaseWaitTime)
        {
            _navMeshAgent.SetDestination(detectionTargetTransform.position);
            _enemyController.SetState(EEnemyState.Chase);
        }
        // Idle > Patrol 상태로 전환
        else if (_waitTime > _enemyController.PatrolWaitTime)
        {
            var randomValue = Random.Range(0, 100);
                
            if (randomValue < _enemyController.PatrolChance)
            {
                var patrolPosition = FindRandomPatrolPosition();
                    
                // 정찰 위치가 현 위치에서 2unit 이상 벗어날 경우 정찰 시작
                float distance = Vector3.SqrMagnitude(patrolPosition - _enemyController.transform.position);
                float minimumDistance = _navMeshAgent.stoppingDistance + 2;
                    
                if (distance > (minimumDistance * minimumDistance))
                {
                    // 정찰 시작
                    _navMeshAgent.SetDestination(patrolPosition);
                    _enemyController.SetState(EEnemyState.Patrol);
                }
            }
            _waitTime = 0f;
        }
        _waitTime += Time.deltaTime;
    }

    public void Exit()
    {
        _animator.SetBool(EnemyAnimPramIdle, false);
    }

    // 정찰 목적지 반환
    Vector3 FindRandomPatrolPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10f;
        randomDirection += _enemyController.transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return _enemyController.transform.position;
    }
}