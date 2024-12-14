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
            Vector3 childLocalPosition = GetRunnerLocalPosition(i);
            transform.GetChild(i).localPosition = childLocalPosition;
        }
    }

    private Vector3 GetRunnerLocalPosition(int index)
    {
        float r = Mathf.Sqrt(index) * radiusFactor;
        float theta = index * Global.GOLDEN_ANGLE * Mathf.Deg2Rad;

        // Chuyển từ toạ độ cực (r, θ) sang toạ độ Đề-các (Descartes)
        float x = r * Mathf.Cos(theta);
        float z = r * Mathf.Sin(theta);

        return new Vector3(x, 0, z);
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

    private void AddRunners(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject runner = Instantiate(runnerPrefab, transform);
            runner.GetComponentInParent<SquadAnimator>().Run();
        }
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
