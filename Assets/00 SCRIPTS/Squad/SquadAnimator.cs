using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadAnimator : MonoBehaviour
{
    [SerializeField] private Transform runnersParent;

    private void Start()
    {
        if (runnersParent == null)
            runnersParent = GameManager.Instance.RunnersParent;
    }

    public void Idle()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Animator runnerAnimator = runnersParent.GetChild(i).GetComponent<Runner>().GetAnimator();
            runnerAnimator.Play("Idle");
        }
    }

    public void Run()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Animator runnerAnimator = runnersParent.GetChild(i).GetComponent<Runner>().GetAnimator();
            runnerAnimator.Play("Run");
        }
    }
}
