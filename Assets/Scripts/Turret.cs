using System;
using UnityEngine;
using System.Collections.Generic;

public class Turret : MonoBehaviour
{

    [Header("Turret Stats")]
    public float range = 5f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float _fireCooldown;
    private Transform _target;

    void Update()
    {
       // look for target if none
       if(_target == null || Vector3.Distance(transform.position, _target.position) > range)
        {
            FindTarget();
        }

        //Aim and shoot if target
        if (_target != null) 
        {
            LockIn();

            _fireCooldown -= Time.deltaTime;
            if (_fireCooldown < 0 )
            {
                Shoot();
                _fireCooldown = 1f / fireRate;
            }
        }
    }

    void FindTarget()
    {
        //get all enemies in scene 

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDist = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach(var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist && dist <= range)
            {
                closestDist = dist;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            _target = closestEnemy.transform;
        }
    }

    void LockIn() //aim at target
    {
        Vector3 dir = (_target.position - transform.position).normalized;
        dir.y = 0; //keep turret horizontal
        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.SetTarget(_target);
    }
}
