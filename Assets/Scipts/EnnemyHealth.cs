using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;  //vie du GO à l'instantiation
    [Tooltip("Adds amount to max hitpoints when enemy dies")]
    [SerializeField] int difficultRamp = 1;
    int currentHitPoints;  //vie mise à jour

    Enemy enemy;

    private void Start() {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        //set les PV actuels
        currentHitPoints = maxHitPoints;
    }

    private void OnParticleCollision(GameObject other) {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHitPoints --;
        if(currentHitPoints <= 0){
            enemy.RewardGold();
            gameObject.SetActive(false);
            maxHitPoints += difficultRamp;
        }
    }
}
