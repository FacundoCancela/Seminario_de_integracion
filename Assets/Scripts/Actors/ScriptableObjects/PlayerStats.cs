
using UnityEngine;

[CreateAssetMenu(fileName = "ActorStats", menuName = "ScriptableObjects/ActorStats", order = 1)]
public class PlayerStats : ScriptableObject
{

    //Player stats
    public int money = 0;
    public int maxHealth = 100;
    public int damageMultiplier = 1;
    public int movementSpeed = 1;
    public int experience;
    public int actualLevel;
    public float attackSpeed = 1;
    public float attackRange;

    //WeaponStats

    public int swordDamage = 2;
    public int projectileDamage = 1;       
    
}