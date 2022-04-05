using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _tankIdle = null;
    [SerializeField] private AudioClip _tankDrive = null;
    [SerializeField] private AudioClip _shoot = null;
    [SerializeField] private AudioClip _explosion = null;

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

    public void ChangeTankSound(AudioSource audioSource)
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

    public void Shoot(Vector3 position)
    {
        GameObject shootEffect = new GameObject();
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
        audio.clip = _shoot;
        audio.Play();
        
        Destroy(explosionEffect, 2f);
    }
}