using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Player stats
    public int maxHealth;
    public int damageMultiplier;
    public int money;
    public int movementSpeed;
    public int actualLevel;
    public float attackSpeed;
    public float attackRange;

    //WeaponStats

    public int swordDamage;
    public int projectileDamage;
    public int orbitDamage;
    public int orbitalSpeed;

    //UnlockedWeapons
    public bool basicSlashUnlocked;
    public bool bigSlashUnlocked;
    public bool orbitalWeaponUnlocked;

    //Shop prices
    public int upgradeCost;
   


}
