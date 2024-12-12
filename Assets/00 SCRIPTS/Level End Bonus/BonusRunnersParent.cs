using System;
using System.Collections.Generic;
using UnityEngine;

public class BonusRunnersParent : MonoBehaviour
{
    [Header("Events")]
    public static Action<int> OnLineDropped;

    [Header("Settings")]
    [SerializeField] private float xSpacing; [SerializeField]
    private float rewardMultiplier = 36f;
    [SerializeField] private float bonusTriggerThreshold = 2f;
    [SerializeField] private float lineHeight = 1.7f;
    private List<RunnerData> runnersData = new List<RunnerData>();

    [Header("Bonus Detection")]
    private EndStairsBonus endStairsBonus;
    private int currentStairBonusIndex;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        SetupRunnersData();
        ArrangeFormation();
        currentStairBonusIndex = 0;
    }

    private void SetupRunnersData()
    {
        runnersData.Clear();

        int soldierCount = transform.childCount;
        int lineCount = Mathf.FloorToInt((-1 + Mathf.Sqrt(1 + 8 * soldierCount)) / 2);
        int soldiersInFirstLayer = Mathf.CeilToInt((-1 + Mathf.Sqrt(1 + 8 * soldierCount)) / 2);

        for (int i = 0; i < soldierCount; i++)
        {
            int soldierLine = GetSoldierLine(i, lineCount, soldiersInFirstLayer);
            int soldiersInPreviousLines = GetSoldiersInPreviousLines(soldierLine, soldiersInFirstLayer);

            int soldiersThisLine = soldiersInFirstLayer - soldierLine;
            int soldierIndexInThisLine = i - soldiersInPreviousLines;

            Vector3 soldierTargetLocalPosition = CalculatePosition(soldiersThisLine, soldierIndexInThisLine, soldierLine);
            Runner runner = transform.GetChild(i).GetComponent<Runner>();

            runnersData.Add(new RunnerData(runner, soldierTargetLocalPosition, soldierLine));
        }
    }

    private int GetSoldierLine(int index, int lineCount, int soldiersInFirstLayer)
    {
        int soldierLine = 0;
        int remainingIndex = index;

        for (int j = 0; j < lineCount; j++)
        {
            remainingIndex -= (soldiersInFirstLayer - j);
            if (remainingIndex < 0)
                break;
            soldierLine++;
        }

        return soldierLine;
    }

    private int GetSoldiersInPreviousLines(int soldierLine, int soldiersInFirstLayer)
    {
        int total = 0;
        for (int k = 0; k < soldierLine; k++)
        {
            total += (soldiersInFirstLayer - k);
        }
        return total;
    }

    private Vector3 CalculatePosition(int soldiersThisLine, int soldierIndexInThisLine, int soldierLine)
    {
        float soldierX = (-((float)soldiersThisLine / 2f) * xSpacing + xSpacing / 2) + xSpacing * soldierIndexInThisLine;
        float soldierY = soldierLine * lineHeight;
        return new Vector3(soldierX, soldierY, 0);
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameState())
        {
            HandleEndBonusState();
            CheckLevelCompletion();
        }
    }

    private void HandleEndBonusState()
    {
        if (endStairsBonus == null)
            endStairsBonus = FindObjectOfType<EndStairsBonus>();

        if (endStairsBonus != null)
        {
            Transform nextStairs = endStairsBonus.transform.GetChild(currentStairBonusIndex);

            if (transform.position.z > nextStairs.position.z - bonusTriggerThreshold)
            {
                DropRunnersLine(currentStairBonusIndex);
                OnLineDropped?.Invoke(currentStairBonusIndex);
                currentStairBonusIndex++;
            }
        }
    }

    private void DropRunnersLine(int lineIndex)
    {
        foreach (RunnerData data in runnersData)
        {
            if (data.line == lineIndex)
                data.runner.Stop();
        }
    }

    private void ArrangeFormation()
    {
        for (int i = 0; i < runnersData.Count; i++)
        {
            runnersData[i].runner.AssignPyramidLocalPosition(runnersData[i].position);
        }
    }

    private void CheckLevelCompletion()
    {
        if (transform.childCount <= 0)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        if (endStairsBonus == null)
            return;

        int bonusIndex = Mathf.Max(0, currentStairBonusIndex - 2);
        int rewardCoins = (int)(endStairsBonus.GetBonus(bonusIndex) * rewardMultiplier);

        DataManager.Instance.AddCoins(rewardCoins);
        UIManager.setLevelCompleteDelegate?.Invoke(rewardCoins);
    }
}

[System.Serializable]
public struct RunnerData
{
    public Runner runner;
    public Vector3 position;
    public int line;

    public RunnerData(Runner runner, Vector3 position, int line)
    {
        this.runner = runner;
        this.position = position;
        this.line = line;
    }
}
