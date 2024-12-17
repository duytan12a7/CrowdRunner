using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSelector : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Runner runner;
    [SerializeField] private Transform skinsParent;
    [SerializeField] private Renderer[] skinsRenderers;

    public void SelectRunner(int runnerIndex)
    {
        for (int i = 0; i < skinsParent.childCount; i++)
        {
            if (i == runnerIndex)
            {
                skinsParent.GetChild(i).gameObject.SetActive(true);
                runner.SetAnimator(skinsParent.GetChild(i).GetComponent<Animator>());
            }
            else
            {
                skinsParent.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void DisableRenderer()
    {
        foreach (Renderer renderer in skinsRenderers)
            renderer.enabled = false;
    }

    public Color GetColor() => skinsRenderers[0].material.GetColor("_BaseColor");
}
