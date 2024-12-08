using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    private Transform targetRunner;

    private enum State { Idle, Running }

    private State state;

    private void Update()
    {
        StateManage();
    }

    private void StateManage()
    {
        switch (state)
        {
            case State.Idle:
                FindTargetRunner();
                break;
            case State.Running:
                AttackRunner();
                break;
        }
    }

    private void StartRunningToTarget()
    {
        GetComponent<Animator>().Play("Run");
        state = State.Running;
    }

    private void FindTargetRunner()
    {
        Collider[] detectedRunners = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedRunners.Length; i++)
        {
            if (detectedRunners[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget() || targetRunner != null)
                    continue;

                runner.SetTarget();
                targetRunner = runner.transform;
                PlayerController.Instance.SetMoveSpeed(4f);

                StartRunningToTarget();
            }
        }
    }

    private void AttackRunner()
    {
        if (targetRunner == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, Time.deltaTime * moveSpeed);
        transform.forward = (targetRunner.transform.position - transform.position).normalized;

        if (Vector3.Distance(transform.position, targetRunner.position) < 1.5f)
        {
            Destroy(gameObject);
            Destroy(targetRunner.gameObject);
        }
    }
}
