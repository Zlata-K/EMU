﻿using System;
using UnityEngine;
using WeaponsSystem.MonoBehaviours;

namespace EnemySystem.ScriptableObjects.EnemyAttackStrategies
{
    [CreateAssetMenu(fileName = "GunnerAttackStrategy",
        menuName = "ScriptableObjects/EnemyAttack/Gunner", order = 3)]
    public class GunnerStrategy : EnemyAttackStrategy
    {
        #region Interface Variables

        [SerializeField] private float shootDistanceRangeMax = 15;
        [SerializeField] private float shootHeightRangeMax = 0.1f;

        #endregion

        /**
         * Shoot the target if they are a certain distance away
         * In this case, we don't need the hasCollided bool, so we will return false
         */
        public override void Attack(GameObject player, GameObject emu, int damageGiven)
        {
            if (Vector2.Distance(player.transform.position, emu.transform.position) < shootDistanceRangeMax)
            {
                var weapon = emu.GetComponentInChildren<WeaponBehaviourScript>();
                if (weapon.CurrentMagazineAmmunition == 0)
                {
                    weapon.WeaponData.ShootStrategy.Reload(weapon);
                }
                weapon.WeaponData.ShootStrategy.Shoot(weapon);
            }
        }
    }
}