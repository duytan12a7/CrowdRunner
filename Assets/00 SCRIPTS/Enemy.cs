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

    private State state = State.Idle;

    private void Update()
    {
        StateManage();
    }

    private void StateManage()
    {
        switch (state)
        {
            case State.Idle:
                SearchForTarget();
                break;
            case State.Running:
                RunTowardsTarget();
                break;
        }
    }

    private void StartRunningToTarget()
    {
        GetComponent<Animator>().Play("Run");
        state = State.Running;
    }

    private void SearchForTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())
                    continue;

                runner.SetTarget();
                targetRunner = runner.transform;
                
                StartRunningToTarget();
            }
        }
    }

    private void RunTowardsTarget()
    {
        if (targetRunner == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, targetRunner.position) < 0.1f)
        {
            Destroy(gameObject);
            Destroy(targetRunner.gameObject);
        }
    }
}
