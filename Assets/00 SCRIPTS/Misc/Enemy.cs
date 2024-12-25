using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State { Idle, Running, Attacking, Dead }
    private State state;

    [Header(" Detection ")]
    [SerializeField] private float detectionDistance;
    [SerializeField] private LayerMask runnersLayer;
    private Runner targetRunner;

    [Header(" Elements ")]
    [SerializeField] private Renderer renderer;
    [SerializeField] private Animator animator;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    private float attackTimer;

    [Header(" Events")]
    public static Action<Vector3, Color> OnEnemyDied;

    private void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        ManageEnemyState();

        if (targetRunner == null)
            FindTargetRunner();
        else
            AttackRunner();
    }

    private void ManageEnemyState()
    {
        switch (state)
        {
            case State.Idle:
                FindTargetRunner();
                break;

            case State.Running:
                GoTowardsTarget();
                break;

            case State.Attacking:
                AttackRunner();
                break;

            case State.Dead:
                break;
        }
    }

    private void FindTargetRunner()
    {
        Collider[] detectedRunners = Physics.OverlapSphere(transform.position, detectionDistance, runnersLayer);

        if (detectedRunners.Length <= 0) return;

        for (int i = 0; i < detectedRunners.Length; i++)
        {
            Runner currentRunner = detectedRunners[i].GetComponent<Runner>();
            if (currentRunner.IsTarget()) continue;

            currentRunner.SetTarget();
            targetRunner = currentRunner;
            StartMoving();
            break;
        }
    }

    private void GoTowardsTarget()
    {
        GetComponent<Rigidbody>().velocity = (targetRunner.transform.position - transform.position).normalized * moveSpeed;

        transform.forward = (targetRunner.transform.position - transform.position).normalized;

        if (Vector3.Distance(transform.position, targetRunner.transform.position) < 1f)
            SetAttackingState();
    }

    private void SetAttackingState()
    {
        state = State.Attacking;

        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void AttackRunner()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= 1f)
        {
            targetRunner.Explode();
            Explode();

            state = State.Dead;
        }
    }

    private void StartMoving()
    {
        animator.Play("Run");
        transform.parent = null;

        state = State.Running;
    }

    private void Explode()
    {
        OnEnemyDied?.Invoke(transform.position, renderer.material.GetColor("_BaseColor"));
        Destroy(gameObject);
    }
}
