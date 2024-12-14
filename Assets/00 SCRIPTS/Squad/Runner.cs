using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    enum RunnerState { Running, Bonus, Stopped }
    private RunnerState runnerState;

    [Header(" Elements ")]
    [SerializeField] private SquadSelector squadSelector;
    [SerializeField] private Animator animator;
    private bool isTarget;

    [Header(" Bonus Settings ")]
    private Vector3 targetPyramidLocalPosition;

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
        isTarget = true;
    }

    public bool IsTarget() => isTarget;

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

    public Animator GetAnimator() => animator;

    public Animator SetAnimator(Animator animator) => this.animator = animator;
}
