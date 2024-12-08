using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [Header(" Sounds ")]
    [SerializeField] private AudioSource doorHitSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource runnerDieSound;

    [Header(" Events ")]
    public static Action OnLevelComplete;
    public static Action OnGameOver;
    public static Action OnRunnerDie;

    private void Start()
    {
        PlayerDetection.OnDoorsHit += PlayDoorHitSound;
        OnLevelComplete += PlayLevelCompleteSound;
        OnGameOver += PlayGameOverSound;
        OnRunnerDie += PlayRunnerDieSound;
    }

    private void OnDestroy()
    {
        PlayerDetection.OnDoorsHit -= PlayDoorHitSound;
        OnLevelComplete -= PlayLevelCompleteSound;
        OnGameOver -= PlayGameOverSound;
        OnRunnerDie -= PlayRunnerDieSound;
    }

    private void PlayDoorHitSound() => doorHitSound.Play();

    private void PlayLevelCompleteSound() => levelCompleteSound.Play();

    private void PlayGameOverSound() => gameOverSound.Play();

    private void PlayRunnerDieSound() => runnerDieSound.Play();
}
