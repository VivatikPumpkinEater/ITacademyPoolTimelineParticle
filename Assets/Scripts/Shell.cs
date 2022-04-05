using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]
public class Shell : MonoBehaviour
{
    [SerializeField] private ParticleSystem _boomEffect = null;
    [SerializeField] private ParticleSystem _smokeTrail = null;

    [SerializeField] private Transform _trailPosition = null;

    [SerializeField] private float _speed = 100f;
    [SerializeField] private float _explosionRadius = 2f;
    [SerializeField] private float _explosionForce = 1000f;

    [SerializeField] private LayerMask _ignoreLayer;

    private bool _startShell = true;
    
    private PoolObject _poolObj = null;
    private ParticleSystem _trail = null;

    private PoolObject _poolObject
    {
        get => _poolObj = _poolObj ?? GetComponent<PoolObject>();
    }

    private Rigidbody _rigidbody = null;

    private Rigidbody _rb
    {
        get => _rigidbody = _rigidbody ?? GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = (transform.forward * _speed) + transform.up * -10f;

        if (_startShell)
        {
            _startShell = false;
            _trail = Instantiate(_smokeTrail, _trailPosition);
            _trail.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.layer != _ignoreLayer)
        {
            var boom = Instantiate(_boomEffect, transform.position, Quaternion.identity);
            boom.Play();
            boom.gameObject.AddComponent<Destroyer>();

            _trail.transform.parent = null;
            _trail.gameObject.AddComponent<Destroyer>();

            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;

            _startShell = true;
            
            SoundManager.Instance.Explosion(transform.position);
            
            _poolObject.ReturnToPool();
        }
    }
}