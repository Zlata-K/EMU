﻿using EnemySystem.Monobehaviours;
using UnityEngine;

namespace EnemySystem.ScriptableObjects.EnemyMovementStrategies
{
    [CreateAssetMenu(fileName = "NewFollowTargetStrategy",
        menuName = "ScriptableObjects/EnemyMovement/Follow", order = 3)]
    public class FollowTargetStrategy : EnemyMovementStrategy
    {
        #region Interface Variables

        [SerializeField] private float followSpeed = 4;
        [SerializeField] private float followRange = 10;
        [SerializeField] private float threshold = 1.25f;

        #endregion

        public override bool Move(Transform emuTransform, Transform playerTransform)
        {
            //This prevents the emu to be on top of the player
            if (Vector2.Distance(emuTransform.position, playerTransform.position) < threshold) return false;
            if (playerTransform != null &&
                (Vector2.Distance(emuTransform.position, playerTransform.position) < followRange  ||
                 emuTransform.gameObject.GetComponent<EnemyController>().GotHit()))
            {
                emuTransform.gameObject.GetComponent<Animator>().SetBool("IsMoving", true);
                emuTransform.position =
                    Vector2.MoveTowards(emuTransform.position, playerTransform.position, followSpeed * Time.deltaTime);
                return true;
            }
            else
            {
                emuTransform.gameObject.GetComponent<Animator>().SetBool("IsMoving", false);
                return false;
            }
        }
    }
}