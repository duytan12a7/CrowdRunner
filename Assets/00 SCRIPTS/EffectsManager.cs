using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [Header(" Events ")]
    public static Action<int> OnPileOfCoin;

    [Header(" Particles")]
    [SerializeField] private ParticleSystem runnerSplashParticles;
    [SerializeField] private ParticleSystem enemySplashParticles;

    [Header(" Effects ")]
    [SerializeField] private Transform pileOfCoinParent;
    private Vector3[] initialPos;
    private Quaternion[] initialRotation;

    private void Awake()
    {
        Runner.OnRunnerDied += PlaySplashParticle;
        Enemy.OnEnemyDied += PlayEnemySplashParticle;
        OnPileOfCoin += RewardPileOfCoin;
    }

    private void OnDestroy()
    {
        Runner.OnRunnerDied -= PlaySplashParticle;
        Enemy.OnEnemyDied -= PlayEnemySplashParticle;
        OnPileOfCoin -= RewardPileOfCoin;
    }

    private void Start()
    {
        initialPos = new Vector3[10];
        initialRotation = new Quaternion[10];

        for (int i = 0; i < pileOfCoinParent.childCount; i++)
        {
            initialPos[i] = pileOfCoinParent.GetChild(i).position;
            initialRotation[i] = pileOfCoinParent.GetChild(i).rotation;
        }
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

    public void RewardPileOfCoin(int coinReward)
    {
        Reset();
        float delay = 0f;

        pileOfCoinParent.gameObject.SetActive(true);

        for (int i = 0; i < pileOfCoinParent.childCount; i++)
        {
            Transform child = pileOfCoinParent.GetChild(i);

            child.DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);
            child.GetComponent<RectTransform>().DOAnchorPos(new Vector2(460f, 780), 1f).SetDelay(delay + 0.5f).SetEase(Ease.OutBack);

            if (i == pileOfCoinParent.childCount - 1)
                child.DOScale(0f, 0.3f).SetDelay(delay + 1f).SetEase(Ease.OutBack).OnComplete(() => DataManager.Instance.AddCoins(coinReward));
            else
                child.DOScale(0f, 0.3f).SetDelay(delay + 1f).SetEase(Ease.OutBack);

            delay += 0.2f;
        }
    }

    private void Reset()
    {
        for (int i = 0; i < pileOfCoinParent.childCount; i++)
        {
            pileOfCoinParent.GetChild(i).position = initialPos[i];
            pileOfCoinParent.GetChild(i).rotation = initialRotation[i];
        }
    }
}
