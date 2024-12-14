using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    enum RunnerState { Running, Bonus, Stopped }
    private RunnerState runnerState;

    [Header(" Elements ")]
    [SerializeField] private Animator animator;
    [SerializeField] private Collider collider;
    [SerializeField] private RunnerSelector runnerSelector;
    private bool targeted;

    [Header(" Bonus Settings ")]
    private Vector3 targetPyramidLocalPosition;

    [Header(" Effects ")]
    [SerializeField] private ParticleSystem explodeParticles;


    [Header(" Events ")]
    public static Action<Runner> OnRunnerDied;

    void Start()
    {
        runnerState = RunnerState.Running;
    }

    // Update is called once per frame
    void Update()
    {
        ManageRunnerState();
    }

    private void ManageRunnerState()
    {
        switch (runnerState)
        {
            case RunnerState.Running:
                // ManageRunningState();
                break;

            case RunnerState.Bonus:
                ManageBonusState();
                break;
        }
    }
    public void SetTarget()
    {
        targeted = true;
    }

    public bool IsTarget() => targeted;

    public void Stop()
    {
        runnerState = RunnerState.Stopped;

        transform.SetParent(null);
    }

    private void ManageBonusState()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPyramidLocalPosition, .025f);
    }

    public void AssignPyramidLocalPosition(Vector3 position)
    {
        GetComponent<Rigidbody>().isKinematic = true;

        targetPyramidLocalPosition = position;

        runnerState = RunnerState.Bonus;
    }

    public void Explode()
    {
        collider.enabled = false;
        runnerSelector.DisableRenderer();

        explodeParticles.Play();

        transform.parent = null;

        OnRunnerDied?.Invoke(this);

        Destroy(gameObject, 3);
    }

    public Animator GetAnimator() => animator;

    public Animator SetAnimator(Animator animator) => this.animator = animator;

    public Color GetColor() => runnerSelector.GetColor();
}
