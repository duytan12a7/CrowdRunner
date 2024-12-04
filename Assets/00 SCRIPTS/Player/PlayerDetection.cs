using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{

    private void Update()
    {
        if (GameManager.Instance.IsGameState())
            DetectedCollider();
    }

    private void DetectedCollider()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1);

        if (colliders.Length < 1) return;

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<Doors>(out Doors doors))
            {
                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                doors.Disable();

                GameManager.Instance.CrowdSystem.ApplyBonus(bonusType, bonusAmount);
            }
            else if (collider.tag == Global.FINISH_TAG)
            {
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);

                GameManager.Instance.SetGameState(GameManager.GameState.LevelComplete);
                // SceneManager.LoadScene(0);
            }
        }
    }
}
