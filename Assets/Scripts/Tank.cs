using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _speedRotate = 100f;
    [SerializeField] private float _gravity = -10f;

    [SerializeField] private Transform _pewPoint;
    
    [SerializeField] private Pool _pool = null;

    private bool _idle = false, _drive = false;
    
    private AudioSource _audioSource = null;

    private AudioSource _audio
    {
        get => _audioSource = _audioSource ?? GetComponent<AudioSource>();
    }
    
    private CharacterController _characterController = null;

    private CharacterController _controller
    {
        get => _characterController = _characterController ?? GetComponent<CharacterController>();
    }

    private void Update()
    {

        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.Shoot(transform.position);
            
            Shoot();
        }
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            Vector3 movement = (transform.forward * vertical * _speed) + (Vector3.up * _gravity);

            _controller.Move(movement * Time.deltaTime);
            _controller.transform.Rotate(Vector3.up * horizontal * _speedRotate * Time.deltaTime);

            if (!_drive)
            {
                SoundManager.Instance.TankStatus = SoundManager.EtankStatus.drive;
                SoundManager.Instance.ChangeTankSound(_audio);
                _drive = true;
                _idle = false;
            }
        }
        else if(!_idle)
        {
            SoundManager.Instance.TankStatus = SoundManager.EtankStatus.idle;
            SoundManager.Instance.ChangeTankSound(_audio);
            _idle = true;
            _drive = false;
        }
    }

    private void Shoot()
    {
        _pool.GetFreeElement(_pewPoint);
    }
}