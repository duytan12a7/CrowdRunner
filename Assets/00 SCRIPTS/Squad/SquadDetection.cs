using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SquadDetection : MonoBehaviour
{
    [Header(" Events ")]
    public static Action OnDoorsHit;
    public static Action OnFinishLineCrossed;

    private void Update()
    {
        if (GameManager.Instance.IsGameState())
            DetectedCollider();
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
}
