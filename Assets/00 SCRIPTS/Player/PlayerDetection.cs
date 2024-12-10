using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDetection : MonoBehaviour
{
    [Header(" Events ")]
    public static Action OnDoorsHit;

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

                GameManager.Instance.CrowdSystem.ApplyBonus(bonusType, bonusAmount);

                OnDoorsHit?.Invoke();
            }
            else if (collider.tag == Global.FINISH_TAG)
            {
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);

                GameManager.Instance.SetGameState(GameManager.GameState.LevelComplete);
            }
        }
    }
}
