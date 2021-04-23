using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private float _fireRateSecondary = 1f;
    [SerializeField] private GameObject _bulletPrefab = default;
    [SerializeField] private Transform _projectileSpawnpoint = default;
    [SerializeField] private ScriptableAudioEvent _pistolAudioEvent = default;
    [SerializeField] private ScriptableAudioEvent _shotgunAudioEvent = default;
    
    private float _lastTimeShot;
    private float _lastTimeShotSecondary;
    private AudioSource _shootingAudioSource;
    
    private void Awake()
    {
        _shootingAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && CanShoot())
        {
            Shoot();
        }
        
        if (Input.GetMouseButton(1) && CanShootSecondary())
        {
            ShootSecondary();
        }
    }

    private void Shoot()
    {
        GameObject inst = Instantiate(_bulletPrefab, _projectileSpawnpoint.transform.position, Quaternion.identity);
        _pistolAudioEvent.Play(_shootingAudioSource);
        _lastTimeShot = Time.time;
    }
    
    private void ShootSecondary()
    {
        GameObject inst = Instantiate(_bulletPrefab, _projectileSpawnpoint.transform.position, Quaternion.identity);
        _shotgunAudioEvent.Play(_shootingAudioSource);
        _lastTimeShotSecondary = Time.time;
    }

    private bool CanShoot()
    {
        return _lastTimeShot + (1 / _fireRate) < Time.time; 
    }

    private bool CanShootSecondary()
    {
        return _lastTimeShotSecondary + (1 / _fireRateSecondary) < Time.time; 
    }
}