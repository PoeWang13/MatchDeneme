using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Canvas_Manager : MonoSingletion<Canvas_Manager>
{
    #region Animation
    #endregion

    #region Music
    public AudioMixer sound;
    public void MusicGenel(float value)
    {
        sound.SetFloat("Genel", value);
    }
    public void MusicBackGround(float value)
    {
        sound.SetFloat("BackGround", value);
    }
    public void MusicUI(float value)
    {
        sound.SetFloat("UI", value);
    }
    public void MusicEffect(float value)
    {
        sound.SetFloat("Effect", value);
    }
    public void MusicWeapon(float value)
    {
        sound.SetFloat("Weapon", value);
    }
    public void MusicBullet(float value)
    {
        sound.SetFloat("Bullet", value);
    }
    #endregion

    #region Panel
    public void OpenClosePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
    #endregion
}