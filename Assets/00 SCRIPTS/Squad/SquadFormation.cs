using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SquadFormation : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject groupAmountBubble;
    [SerializeField] private TextMeshPro squadAmountText;
    [SerializeField] private GameObject runnerPrefab;
    [SerializeField] private BonusRunnersParent bonusRunnersParent;

    [Header(" Settings ")]
    [SerializeField] private float radiusFactor;

    private void Awake()
    {
        SquadDetection.OnFinishLineCrossed += EnableBonusParentScript;
    }

    private void OnDestroy()
    {
        SquadDetection.OnFinishLineCrossed -= EnableBonusParentScript;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameState())
            return;

        FermatSpiralPlace();

        squadAmountText.text = transform.childCount.ToString();

        if (transform.childCount < 1)
            GameManager.Instance.SetGameState(GameManager.GameState.GameOver);
    }

    private void FermatSpiralPlace()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 runnerTargetWorldPosition = GetFermatPosition(i);

            Transform currentRunner = transform.GetChild(i);

            Vector3 velocityDirection = runnerTargetWorldPosition - currentRunner.position;

            Rigidbody runnerRig = currentRunner.GetComponent<Rigidbody>();

            runnerRig.velocity = Vector3.Lerp(runnerRig.velocity, velocityDirection, 0.1f);
        }
    }

    public float GetSquadRadius() => radiusFactor * Mathf.Sqrt(transform.childCount);

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch (bonusType)
        {
            case BonusType.Add:
                AddRunners(bonusAmount);
                break;

            case BonusType.Subtract:
                SubtractRunners(bonusAmount);
                break;

            case BonusType.Multiply:
                int runnersToAdd = (transform.childCount * bonusAmount) - transform.childCount;
                AddRunners(runnersToAdd);
                break;

            case BonusType.Divide:
                int runnerToSubtract = transform.childCount - (transform.childCount / bonusAmount);
                SubtractRunners(runnerToSubtract);
                break;
        }
    }

    private void EnableBonusParentScript()
    {
        groupAmountBubble.SetActive(false);

        bonusRunnersParent.enabled = true;
        enabled = false;
    }

    public void AddRunners(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPosition = GetFermatPosition(transform.childCount);

            Runner runner = Instantiate(runnerPrefab, spawnPosition, Quaternion.identity, transform).GetComponent<Runner>();

            runner.GetComponentInParent<SquadAnimator>().Run();

            runner.name = "Runner_" + runner.transform.GetSiblingIndex();
        }
    }

    private Vector3 GetFermatPosition(int index)
    {
        float r = Mathf.Sqrt(index) * radiusFactor;
        float theta = index * Global.GOLDEN_ANGLE * Mathf.Deg2Rad;

        float x = r * Mathf.Cos(theta);
        float z = r * Mathf.Sin(theta);

        Vector3 runnerLocalPosition = new(x, 0, z);
        Vector3 runnerTargetWorldPosition = transform.TransformPoint(runnerLocalPosition);

        return runnerTargetWorldPosition;
    }

    private void SubtractRunners(int amount)
    {
        int runnersAmount = transform.childCount;

        if (amount > runnersAmount)
            amount = runnersAmount;

        for (int i = runnersAmount - 1; i > runnersAmount - amount; i--)
        {
            Transform runnerToDisable = transform.GetChild(i);
            Destroy(runnerToDisable.gameObject);
        }
    }
}
