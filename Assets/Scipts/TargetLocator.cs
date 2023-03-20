using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem projectileParticles;
     Transform target;

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget()
    {
        Enemy[] ennemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in ennemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance){
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }

    private void AimWeapon()    //méthode pour viser
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);
        //targetDistance < Range -> shoot
        if(targetDistance <= range){
            Attack(true);
        } else {
            Attack(false);
        }
    }

    void Attack(bool isActive){     //méthode pour tirer
        var emission = projectileParticles.emission;
        emission.enabled = isActive; 
        
    }
}
