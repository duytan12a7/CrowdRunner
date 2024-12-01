using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private CrowdSystem crowdSystem;

    private void Update()
    {
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

                crowdSystem.ApplyBonus(bonusType, bonusAmount);
            }
            else if (collider.tag == Global.FINISH_TAG)
                SceneManager.LoadScene(0);
        }
    }
}
