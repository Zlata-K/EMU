﻿using System;
using System.Collections;
using Interactables;
using Player;
using ScriptableObjects.WeaponsSystem;
using UnityEngine;

namespace MonoBehaviours.WeaponsSystem
{
    /// <summary>
    /// Controls the behavior of a projectile
    /// </summary>
    // todo: Environment needs to be on different layers, and projectiles only interact with some of the environment layers
    public class ProjectileBehaviourScript : MonoBehaviour
    {
        [SerializeField] private int aliveTime;
    
        private ProjectileData _projectileData;
        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private bool _shouldDamagePlayer;

        /// <summary>
        ///  Initializes parameters of the script.
        /// </summary>
        /// <param name="projectileData"></param> The projectile's data (Scriptable object instance)
        /// <param name="spawnDirection">The direction of the projectile (right or left)</param>
        /// <param name="shouldDamagePlayer">If the projectile was shot by player's weapon, this should be false</param>
        public void Init(ProjectileData projectileData, Vector2 spawnDirection, bool shouldDamagePlayer)
        {
            _projectileData = projectileData;
            _direction = spawnDirection;
            _shouldDamagePlayer = shouldDamagePlayer;

            _rigidbody.isKinematic = true;
            _spriteRenderer.sprite = _projectileData.ProjectileSprite;
        }
    
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            StartCoroutine(WaitForDestroy());
        }

        private void Update()
        {
            transform.Translate(_projectileData.ProjectileSpeed * Time.deltaTime * _direction);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.name);
            
            if (other.CompareTag("Enemy"))
            {
                //todo: Deal damage to enemy
            }
            else if (_shouldDamagePlayer && other.CompareTag("Player"))
            {
                var player = other.GetComponent<Controller>();
                player.LoseHitPoints(_projectileData.ProjectileBaseDamage);
            }
            /*else if (other.CompareTag("ExplosiveBarrel"))
            {
                other.GetComponent<IBreakable>().Break();
            }*/
            else if (other.CompareTag("Ground") || other.CompareTag("Platform"))
            {
                Destroy(gameObject);    
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }

        private IEnumerator WaitForDestroy()
        {
            yield return new WaitForSeconds(aliveTime);
            Destroy(gameObject);
        }
    }
}