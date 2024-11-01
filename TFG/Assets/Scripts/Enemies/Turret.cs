﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    private float cooldownTime;
    [SerializeField] private float distanceAttack = 9f;
    private float attackTimer;
    [SerializeField] private float startTimer = 2.25f;

    [SerializeField] private GameObject bulletProjectile;
    [SerializeField] private Transform shotPoint;

    // Start is called before the first frame update
    void Awake()
    {
        attackTimer = startTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < distanceAttack)
            {
                AdquireTarget();

                Vector3 targetDir = target.position - transform.position;
                float shotAngle = Vector3.Angle(transform.up, targetDir);
                Debug.Log("shotAngle: " + shotAngle);
                if (shotAngle >= 35f && shotAngle <= 170f)
                {
                    Attack();
                }
            }
        }
    }

    private void Attack()
    {
        if (attackTimer <= 0)
        {
            attackTimer = startTimer;
            anim.Play("Shot");
            Invoke("ShotProjectile", 0.3f);
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void ShotProjectile()
    {
        GameObject obj = Instantiate(bulletProjectile, shotPoint) as GameObject;
        AudioManager.Instance.PlaySoundEffect("ThrowObject");
        obj.transform.parent = null;
    }

    private void AdquireTarget()
    {
        float dir = 1f;
        if (transform.position.x > target.position.x)
        {
            dir = -1f;
        }
        CheckDirection(dir);
    }
}
