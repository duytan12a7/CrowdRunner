using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform runnersParent;
    [SerializeField] private GameObject runnerPrefab;

    [Header(" Settings ")]
    [SerializeField] private float radius;

    private void Update()
    {
        PlaceRunners();
    }

    private void PlaceRunners()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Vector3 childLocalPosition = GetRunnerLocalPosition(i);
            runnersParent.GetChild(i).localPosition = childLocalPosition;
        }
    }

    private Vector3 GetRunnerLocalPosition(int index)
    {
        float r = Mathf.Sqrt(index) * radius;
        float theta = index * Global.GOLDEN_ANGLE * Mathf.Deg2Rad;

        // Chuyển từ toạ độ cực (r, θ) sang toạ độ Đề-các (Descartes)
        float x = r * Mathf.Cos(theta);
        float z = r * Mathf.Sin(theta);

        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius() => radius * Mathf.Sqrt(runnersParent.childCount);

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
                int runnersToAdd = (runnersParent.childCount * bonusAmount) - runnersParent.childCount;
                AddRunners(runnersToAdd);
                break;

            case BonusType.Divide:
                int runnerToSubtract = runnersParent.childCount - (runnersParent.childCount / bonusAmount);
                SubtractRunners(runnerToSubtract);
                break;
        }
    }

    private void AddRunners(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(runnerPrefab, runnersParent);
        }
    }

    private void SubtractRunners(int amount)
    {
        int runnersAmount = runnersParent.childCount;

        if (amount > runnersAmount)
            amount = runnersAmount;

        for (int i = runnersAmount - 1; i > runnersAmount - amount; i--)
        {
            Transform runnerToDisable = runnersParent.GetChild(i);
            Destroy(runnerToDisable.gameObject);
        }
    }
}
