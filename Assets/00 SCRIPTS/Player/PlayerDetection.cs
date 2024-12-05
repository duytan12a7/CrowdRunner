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
            }
            else if (collider.tag == Global.FINISH_TAG)
            {
                if (PlayerPrefs.GetInt("level") >= ChunkManager.Instance.GetCountLevel() - 1)
                    PlayerPrefs.SetInt("level", 0);
                else
                    PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);

                GameManager.Instance.SetGameState(GameManager.GameState.LevelComplete);
            }
        }
    }
}
