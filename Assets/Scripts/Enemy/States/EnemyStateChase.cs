using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;
using static Constants;

public class EnemyStateChase : EnemyState, ICharacterState
{
    private float _waitTime;
    
    public EnemyStateChase(EnemyController enemyController, Animator animator, NavMeshAgent navMeshAgent) : base(enemyController, animator, navMeshAgent)
    {
    }

    public void Enter()
    {
        _navMeshAgent.isStopped = false;
        _animator.SetBool(EnemyAnimPramChase, true);
        _waitTime = 0f;
    }

    public void Update()
    {
        var detectionTargetTransform = _enemyController.DetectionTargetInCircle();
        if (detectionTargetTransform)
        {
            // 공격
            if (!_navMeshAgent.pathPending
                && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance
                && _waitTime > _enemyController.AttackWaitTime
                && DetectPlayerInSight(detectionTargetTransform.position))
            {
                _enemyController.SetState(EEnemyState.Attack);
            }
            // 달리기
            if (DetectPlayerInSight(detectionTargetTransform.position) 
                && _navMeshAgent.remainingDistance > _enemyController.MinimumRunDistance)
            {
                _animator.SetFloat(EnemyAnimPramMoveSpeed, 1);
            }
            else
            {
                _animator.SetFloat(EnemyAnimPramMoveSpeed, 0);
            }
            _navMeshAgent.SetDestination(detectionTargetTransform.position);
        }
        else
        {
            _enemyController.SetState(EEnemyState.Idle);
        }

        _waitTime += Time.deltaTime;
    }

    public void Exit()
    {
        _animator.SetBool(EnemyAnimPramChase, false);
    }

    private bool DetectPlayerInSight(Vector3 position)
    {
        var cosTheta = Vector3.Dot(_enemyController.transform.forward,
            (position - _enemyController.transform.position).normalized);
        var angle = Mathf.Acos(cosTheta) * Mathf.Rad2Deg;

        if (angle < _enemyController.DetectionSightAngle)
        {
            return true;
        }
        
        return false;
    }
}
        