using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{

    [Header(" Elements ")]
    [SerializeField] private Animator animator;
    private bool isTarget;

    public void SetTarget()
    {
        isTarget = true;
    }

    public bool IsTarget() => isTarget;


    public Animator GetAnimator() => animator;

    public Animator SetAnimator(Animator animator) => this.animator = animator;
}
