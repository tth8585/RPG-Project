using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class SoundManager 
{
    public enum Sound
    {
        PlayerMove,
        PlayerAttack,
        EnemyHit,
        EnemyDie,
        Treasure,
        ButtonOver,
        ButtonClick,
        PlayerLevelUp,
        GemUsing,
        OpenStash,
        Spell1,
        Spell2,
        Spell3,
        Spell4,
        Teleport,
        ItemPick,
        HelpMe,
        Playerhurt,
    }
    private static Dictionary<Sound, float> soundTimeDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    public static void Init()
    {
        soundTimeDictionary = new Dictionary<Sound, float>();
        soundTimeDictionary[Sound.PlayerMove] = 0f;
    }
    public static void PlaySound(Sound sound,Vector3 pos)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = pos;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.Play();

            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
            UnityEngine.Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }
    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                oneShotAudioSource.outputAudioMixerGroup = Resources.Load<AudioMixerGroup>("Sound/Mixer/MainMixer");
            }

            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }  
    }
    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            case Sound.PlayerMove:
                if (soundTimeDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimeDictionary[sound];
                    float playerMoveTimerMax = 0.05f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }            
            default:
                return true;
        }
    }
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipsArray)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found");
        return null;
    }

    public static void AddButtonSounds(this UI_Button uiButton)
    {
        uiButton.ClickFunc += () => SoundManager.PlaySound(Sound.ButtonClick);
        uiButton.MouseOverOnceFunc += () => SoundManager.PlaySound(Sound.ButtonOver);
    }
}
