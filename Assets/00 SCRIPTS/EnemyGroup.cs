using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyParent;

    [Header(" Settings ")]
    [SerializeField] private float radius;
    [SerializeField] private int amount;

    private bool isEnable = false;

    private void Start()
    {
        GenerateEnemies();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < 30f && !isEnable)
        {
            isEnable = true;
            EnableAllEnemies();
        }
    }

    private void GenerateEnemies()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 childLocalPosition = GetEnemyLocalPosition(i);
            Vector3 childWorldPosition = enemyParent.TransformPoint(childLocalPosition);

            GameObject newEnemy = Instantiate(enemyPrefab, childWorldPosition, Quaternion.identity, enemyParent);
            newEnemy.SetActive(false);
        }

        enemyParent.GetChild(0).gameObject.SetActive(true);
    }

    private void EnableAllEnemies()
    {
        for (int i = 1; i < enemyParent.childCount; i++)
        {
            enemyParent.GetChild(i).gameObject.SetActive(true);
        }
    }

    private Vector3 GetEnemyLocalPosition(int index)
    {
        float r = Mathf.Sqrt(index) * radius;
        float theta = index * Global.GOLDEN_ANGLE * Mathf.Deg2Rad;

        float x = r * Mathf.Cos(theta);
        float z = r * Mathf.Sin(theta);

        return new Vector3(x, 0, z);
    }
}
