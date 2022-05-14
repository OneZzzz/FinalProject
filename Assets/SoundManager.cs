using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public static void PlaySound(string clipName, bool loop = false, float volume = 0.5f, float pitch = 1)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

        audioSource.PlayOneShot(Resources.Load<AudioClip>(clipName));
        if (loop) audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
    }
}
