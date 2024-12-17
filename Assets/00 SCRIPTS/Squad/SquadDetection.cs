using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SquadDetection : MonoBehaviour
{
    [Header(" Events ")]
    public static Action OnEnemiesDetected;
    public static Action OnNoEnemiesDetected;
    public static Action OnDoorsHit;
    public static Action OnFinishLineCrossed;
    [SerializeField] private LayerMask enemiesLayer;
    [SerializeField] private float enemiesDetectionRadius;
    private bool previousEnemiesDetected;

    private void Update()
    {
        if (!GameManager.Instance.IsGameState()) return;
        
        DetectedCollider();
        DetectEnemies();
    }

    private void DetectedCollider()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, 1);

        if (detectedColliders.Length < 1) return;

        foreach (Collider collider in detectedColliders)
        {
            if (collider.gameObject.TryGetComponent<Doors>(out Doors doors))
            {
                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                doors.Disable();

                GameManager.Instance.SquadFormation.ApplyBonus(bonusType, bonusAmount);

                OnDoorsHit?.Invoke();
            }
            else if (collider.gameObject.TryGetComponent<FinishLine>(out FinishLine finishLine))
            {
                finishLine.Disable();

                OnFinishLineCrossed?.Invoke();
            }
        }
    }

    private void DetectEnemies()
    {
        bool enemiesDetected = Physics.OverlapSphere(transform.position, enemiesDetectionRadius, enemiesLayer).Length > 0;

        if (enemiesDetected && !previousEnemiesDetected)
            OnEnemiesDetected?.Invoke();
        else if (!enemiesDetected && previousEnemiesDetected)
            OnNoEnemiesDetected?.Invoke();

        previousEnemiesDetected = enemiesDetected;
    }
}
