using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [Header(" Particles")]
    [SerializeField] private ParticleSystem runnerSplashParticles;
    [SerializeField] private ParticleSystem enemySplashParticles;

    private void Awake()
    {
        Runner.OnRunnerDied += PlaySplashParticle;
        Enemy.OnEnemyDied += PlayEnemySplashParticle;
    }

    private void OnDestroy()
    {
        Runner.OnRunnerDied -= PlaySplashParticle;
        Enemy.OnEnemyDied -= PlayEnemySplashParticle;
    }

    private void PlaySplashParticle(Runner runner)
    {
        Color color = runner.GetColor();
        Vector3 position = runner.transform.position;

        PlaySplashParticle(position, color);
    }

    public void PlaySplashParticle(Vector3 position, Color color)
    {
        runnerSplashParticles.transform.position = position + Vector3.up * 0.01f;

        ParticleSystem.MainModule main = runnerSplashParticles.main;
        main.startColor = color;

        runnerSplashParticles.Play();
    }

    public void PlayEnemySplashParticle(Vector3 position, Color color)
    {
        enemySplashParticles.transform.position = position + Vector3.up * 0.01f;

        ParticleSystem.MainModule main = enemySplashParticles.main;
        main.startColor = color;

        enemySplashParticles.Play();
    }
}
