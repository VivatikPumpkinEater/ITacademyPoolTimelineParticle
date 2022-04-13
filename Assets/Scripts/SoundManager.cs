using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<Clips> _audioClipsses;

    [SerializeField] private List<Source> _audioSourcees;
    /*[SerializeField] private AudioClip _tankIdle = null;
    [SerializeField] private AudioClip _tankDrive = null;
    [SerializeField] private AudioClip _shoot = null;
    [SerializeField] private AudioClip _explosion = null;*/

    public EtankStatus TankStatus;
    public enum EtankStatus
    {
        idle,
        drive
    }
    
    public static SoundManager Instance = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }

        Instance = this;
    }

    public AudioClip GetSound(string key)
    {
        foreach (var clipss in _audioClipsses)
        {
            if (clipss.Key == key)
            {
                return clipss.AudioClip;
            }
        }
        
        Debug.LogError("No sound");
        return null;
    }

    public void GetSound(string key, Vector3 position)
    {
        foreach (var audioSourcee in _audioSourcees)
        {
            if (audioSourcee.Key == key)
            {
                var soundEffect = Instantiate(audioSourcee.AudioSource);
                soundEffect.transform.position = position;

                soundEffect.clip = GetSound(key);
                soundEffect.Play();

                Destroy(soundEffect.gameObject, 2f);
                return;
            }
        }
        
    }

    /*public void ChangeTankSound(AudioSource audioSource)
    {
        switch (TankStatus)
        {
            case EtankStatus.idle:
                audioSource.clip = _tankIdle;
                break;
            case EtankStatus.drive:
                audioSource.clip = _tankDrive;
                break;
        }
        
        audioSource.Play();
    }

    public void Shoot(Vector3 position) // rewrite to key value logic
    {
        GameObject shootEffect = new GameObject(); // Prefab
        shootEffect.transform.position = position;
        
        var audio = shootEffect.AddComponent<AudioSource>();
        audio.clip = _shoot;
        audio.Play();
        
        Destroy(shootEffect, 2f);
    }

    public void Explosion(Vector3 position)
    {
        GameObject explosionEffect = new GameObject();
        explosionEffect.transform.position = position;
        
        var audio = explosionEffect.AddComponent<AudioSource>();
        audio.clip = _explosion;
        audio.Play();
        
        Destroy(explosionEffect, 2f);
    }*/
}

[Serializable]
public struct Clips
{
    public string Key;
    public AudioClip AudioClip;
}

[Serializable]
public struct Source
{
    public string Key;
    public AudioSource AudioSource;
}