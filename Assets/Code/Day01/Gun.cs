using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private GameObject _bulletPrefab = default;
    [SerializeField] private Transform _projectileSpawnpoint = default;
    
    private float _lastTimeShot;
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
    }

    private void Shoot()
    {
        GameObject inst = Instantiate(_bulletPrefab, _projectileSpawnpoint.transform.position, Quaternion.identity);
        _shootingAudioSource.Play();
        _lastTimeShot = Time.time;
    }

    private bool CanShoot()
    {
        return _lastTimeShot + (1 / _fireRate) < Time.time; 
    }
}