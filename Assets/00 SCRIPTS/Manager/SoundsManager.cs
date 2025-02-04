using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [Header(" Sounds ")]
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource doorHitSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource runnerDieSound;

    [Header(" Events ")]
    public static Action OnLevelComplete;
    public static Action OnGameOver;

    private void Start()
    {
        SquadDetection.OnDoorsHit += PlayDoorHitSound;
        OnLevelComplete += PlayLevelCompleteSound;
        OnGameOver += PlayGameOverSound;
        Runner.OnRunnerDied += PlayRunnerDieSound;
    }

    private void OnDestroy()
    {
        SquadDetection.OnDoorsHit -= PlayDoorHitSound;
        OnLevelComplete -= PlayLevelCompleteSound;
        OnGameOver -= PlayGameOverSound;
        Runner.OnRunnerDied -= PlayRunnerDieSound;
    }

    private void PlayDoorHitSound() => doorHitSound.Play();

    private void PlayLevelCompleteSound() => levelCompleteSound.Play();

    private void PlayGameOverSound() => gameOverSound.Play();

    private void PlayRunnerDieSound(Runner runner) => runnerDieSound.Play();

    public void EnableSounds()
    {
        buttonSound.volume = 1;
        doorHitSound.volume = 1;
        levelCompleteSound.volume = 1;
        gameOverSound.volume = 1;
        runnerDieSound.volume = 1;
    }

    public void DisableSounds()
    {
        buttonSound.volume = 0;
        doorHitSound.volume = 0;
        levelCompleteSound.volume = 0;
        gameOverSound.volume = 0;
        runnerDieSound.volume = 0;
    }
}
