﻿using System.Collections.Generic;
using MonoBehaviours.WeaponsSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class InventoryManager : MonoBehaviour
    {
        private Dictionary<InventorySlot, WeaponBehaviourScript> _weaponSlots;
        private InventorySlot _currentActiveWeaponSlot;

        /// <summary>
        /// Public property to access the weapons currently in the inventory. Keys are slots, and values are the
        /// WeaponBehaviourScripts attached to the weapon gameobjects. Values can be null.
        /// </summary>
        public Dictionary<InventorySlot, WeaponBehaviourScript> WeaponSlots => _weaponSlots;
        public InventorySlot CurrentActiveWeaponSlot => _currentActiveWeaponSlot;

        private void Awake()
        {
            _weaponSlots = new Dictionary<InventorySlot, WeaponBehaviourScript>
            {
                {InventorySlot.First, null}, {InventorySlot.Second, null}, {InventorySlot.Throwable, null}
            };

            _currentActiveWeaponSlot = InventorySlot.First;
        }

        private void Update()
        {
            if (_weaponSlots[_currentActiveWeaponSlot] != null)
            {
                _weaponSlots[_currentActiveWeaponSlot].WeaponStateProp = WeaponState.Active;
                var weaponPos = gameObject.transform.position + GetComponent<SpriteRenderer>().sprite.bounds.center;
                _weaponSlots[_currentActiveWeaponSlot].transform.position = weaponPos;
            }
        }

        /// <summary>
        /// Returns the current active Weapon
        /// </summary>
        /// <returns>The current active Weapon</returns>
        public WeaponBehaviourScript GetActiveWeapon()
        {
            return _weaponSlots[_currentActiveWeaponSlot];
        }
        
        public void SwitchActiveWeapon(KeyCode keyPressed)
        {
            switch (keyPressed)
            {
                case KeyCode.Alpha1:
                {
                    _currentActiveWeaponSlot = InventorySlot.First;
                    if (_weaponSlots[InventorySlot.Second] != null)
                        _weaponSlots[InventorySlot.Second].WeaponStateProp = WeaponState.InInventory;
                    break;
                }
                case KeyCode.Alpha2:
                {
                    _currentActiveWeaponSlot = InventorySlot.Second;
                    if (_weaponSlots[InventorySlot.First] != null)
                        _weaponSlots[InventorySlot.First].WeaponStateProp = WeaponState.InInventory;
                    break;
                }
            }
        }

        /// <summary>
        /// Adds a weapon to the inventory.
        /// </summary>
        /// <param name="weapon">The weapon gameobject to add to the inventory</param>
        /// <returns>A boolean indicating if the addition was successful</returns>
        public bool AddWeapon(GameObject weapon)
        {
            var weaponScript = weapon.GetComponent<WeaponBehaviourScript>();
            if (weaponScript == null) return false;
            
            if (weapon.CompareTag("Weapon"))
            {
                var openSlot = GetFirstOpenSlot();
                if (openSlot.HasValue)
                {
                    return AddWeapon(openSlot.Value, weaponScript);
                }
                else
                {
                    return AddWeapon(_currentActiveWeaponSlot, weaponScript);
                }
            } 
            else if (weapon.CompareTag("Throwable"))
            {
                return AddWeapon(InventorySlot.Throwable, weaponScript);
            }
            else
            {
                Debug.Log("Tried to add weapon that didn't have a valid tag.");
                return false; 
            }
        }
        
        /// <summary>
        /// Adds a weapon to the inventory, at the given inventory slot. If there already is a weapon there, drops it.
        /// </summary>
        /// <param name="slot">The inventory slot to add the weapon to</param>
        /// <param name="weaponScript">The WeaponBehaviourScript attached to the weapon gameobject</param>
        /// <returns>A boolean indicating if the addition was successful</returns>
        private bool AddWeapon(InventorySlot slot, WeaponBehaviourScript weaponScript)
        {
            if (_weaponSlots[slot] != null)
            {
                if (_weaponSlots[slot].WeaponData.name.Equals(weaponScript.WeaponData.name))
                {
                    _weaponSlots[slot].CurrentTotalAmmunition += weaponScript.CurrentMagazineAmmunition + weaponScript.CurrentTotalAmmunition;
                    Destroy(weaponScript.gameObject);
                    Debug.Log("Added " + weaponScript.WeaponData.name + " to Inventory slot " + slot);
                    return true;
                }
                else
                {
                    _weaponSlots[slot].WeaponStateProp = WeaponState.OnGround;
                    weaponScript.WeaponStateProp = WeaponState.InInventory;
                    _weaponSlots[slot] = weaponScript;
                    Debug.Log("Added " + weaponScript.WeaponData.name + " to Inventory slot " + slot);
                    return true; 
                }
            }
            else
            {
                weaponScript.WeaponStateProp = WeaponState.InInventory;
                _weaponSlots[slot] = weaponScript;
                Debug.Log("Added " + weaponScript.WeaponData.name + " to Inventory slot " + slot);
                return true;
            }
        }
        
        private InventorySlot? GetFirstOpenSlot()
        {
            if (_weaponSlots[InventorySlot.First] == null)
            {
                return InventorySlot.First;
            }
            else if (_weaponSlots[InventorySlot.Second] == null)
            {
                return InventorySlot.Second;
            }

            return null;
        }
    }

    public enum InventorySlot
    {
        First,
        Second,
        Throwable
    }
}