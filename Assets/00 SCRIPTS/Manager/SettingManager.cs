using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private SoundsManager soundManager;
    [SerializeField] private Sprite optionOffSprite;
    [SerializeField] private Sprite optionOnSprite;
    [SerializeField] private Image soundButtonImage;

    [Header(" Settings ")]
    private bool soundState = true;

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        if (soundState)
            EnableSound();
        else
            DisableSound();
    }

    public void ChangeSoundState()
    {
        if (soundState)
            DisableSound();
        else
            EnableSound();

        soundState = !soundState;
    }

    private void EnableSound()
    {
        soundButtonImage.sprite = optionOnSprite;
        soundManager.EnableSounds();
    }

    private void DisableSound()
    {
        soundButtonImage.sprite = optionOffSprite;
        soundManager.DisableSounds();
    }
}
