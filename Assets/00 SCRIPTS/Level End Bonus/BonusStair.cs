using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusStair : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Renderer stairRenderer;
    [SerializeField] private TextMeshPro bonusText;

    public void Configure(Color stairColor, string bonusString)
    {
        stairRenderer.material.color = stairColor;
        bonusText.text = bonusString;
    }

    private void OnTriggerEnter(Collider col)
    {
        Runner runner = col.GetComponent<Runner>();
        if (!runner) return;

        Animator runnerAnimator = runner.GetAnimator();
        runnerAnimator.Play("Idle");
    }
}
