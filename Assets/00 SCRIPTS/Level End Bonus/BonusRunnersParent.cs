using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BonusRunnersParent : MonoBehaviour
{
    [Header(" Events")]
    public static Action<int> OnLineDropped;

    [Header(" Settings ")]
    [SerializeField] private float xSpacing;
    List<RunnerData> runnersData = new List<RunnerData>();

    [Header(" Bonus Detection ")]
    private EndStairsBonus endStairsBonus;
    private int currentStairBonusIndex = 0;

    private void OnEnable()
    {
        SetupRunnersData();
        ManageFormationNew();
        currentStairBonusIndex = 0;
    }

    private void SetupRunnersData()
    {
        float floatLineCount = (-1 + Mathf.Sqrt(1 + 8 * transform.childCount)) / 2;
        int lineCount = Mathf.FloorToInt(floatLineCount);
        int soldiersInFirstLayer = Mathf.CeilToInt(floatLineCount);
        int soldierCount = transform.childCount;

        for (int i = 0; i < transform.childCount; i++)
        {
            // Let's first determine the soldier line
            int soldierLine = 0;
            int soldierIndex = i;

            for (int j = 0; j < lineCount; j++)
            {
                soldierIndex -= (soldiersInFirstLayer - j);

                if (soldierIndex < 0)
                    break;

                soldierLine++;
            }

            int soldiersInPreviousLines = 0;

            for (int k = 0; k < soldierLine; k++)
                soldiersInPreviousLines += soldiersInFirstLayer - k;


            int soldiersThisLine = soldiersInFirstLayer - soldierLine;
            int soldierIndexInThisLine = i - soldiersInPreviousLines;


            float soldierX = (-((float)soldiersThisLine / 2f) * xSpacing + xSpacing / 2) + xSpacing * soldierIndexInThisLine; //(-((float)soldiersThisLine + xSpacing) / 2) + (soldierIndexInThisLine * xSpacing) + .5f;
            float soldierY = soldierLine * 1.7f;

            Vector3 soldierTargetLocalPosition = new Vector3(soldierX, soldierY, 0);

            runnersData.Add(new RunnerData(transform.GetChild(i).GetComponent<Runner>(), soldierTargetLocalPosition, soldierLine));
        }
    }

    private void Update()
    {

        if (GameManager.Instance.IsGameState())
        {
            ManageEndBonusState();
            CheckForLevelComplete();
        }
    }

    private void ManageEndBonusState()
    {
        if (endStairsBonus == null)
            endStairsBonus = FindObjectOfType<EndStairsBonus>();

        Transform nextStairs = endStairsBonus.transform.GetChild(currentStairBonusIndex);

        if (transform.position.z > nextStairs.position.z - 1.7f)
        {
            //if(currentStairBonusIndex == 0)
            //    DropRunnersLine(currentStairBonusIndex);

            DropRunnersLine(currentStairBonusIndex);

            OnLineDropped?.Invoke(currentStairBonusIndex);

            currentStairBonusIndex++;
        }
    }

    private void DropRunnersLine(int lineIndex)
    {
        for (int i = 0; i < runnersData.Count; i++)
        {
            RunnerData data = runnersData[i];

            if (data.line == lineIndex)
                data.runner.Stop();
        }
    }

    private void ManageFormationNew()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Runner runner = transform.GetChild(i).GetComponent<Runner>();
            runner.AssignPyramidLocalPosition(runnersData[i].position);
        }
    }

    private void CheckForLevelComplete()
    {
        if (transform.childCount <= 0)
            SetLevelComplete();
    }

    private void SetLevelComplete()
    {
        UIManager.setLevelCompleteDelegate?.Invoke();

        Debug.Log("Bonus : " + endStairsBonus.GetBonus(currentStairBonusIndex - 2));

        int rewardCoins = (int)(endStairsBonus.GetBonus(currentStairBonusIndex - 2) * 50);
        // levelCompleteParticleControl.PlayControlledParticles(JetSystems.Utils.GetScreenCenter(), levelCompleteCoinImage, rewardCoins);
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
