using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStairsBonus : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private BonusStair bonusStairPrefab;

    [Header(" Settings ")]
    [SerializeField] private int stairCount;
    [SerializeField] private float stairHeight;
    [SerializeField] private float bonusStep;

    private void Start()
    {
        SpawnStairs();
    }

    private void SpawnStairs()
    {
        for (int i = 0; i < stairCount; i++)
        {
            Vector3 spawnPosition = (transform.position + i * (Vector3.forward * 3 + Vector3.up * stairHeight)) + Vector3.up * stairHeight / 2f;
            BonusStair bonusStairInstance = Instantiate(bonusStairPrefab, spawnPosition, Quaternion.identity, transform);

            float stairBonus = GetBonus(i);
            bonusStairInstance.Configure(Color.HSVToRGB((float)i / stairCount, 0.8f, 0.8f), "x" + stairBonus.ToString());
        }
    }

    private float GetBonus(int lineIndex) => 1 + (lineIndex * bonusStep);
}
