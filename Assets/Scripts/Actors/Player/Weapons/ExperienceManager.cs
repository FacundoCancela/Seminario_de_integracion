using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] PlayerController playerController;
    [SerializeField] LevelUPScreen levelUPScreen;
    [SerializeField] XPBar XpBar;

    public int actualExperience;
    public int maxExperience;
    public int extraExperiencePerLevel;
    public int level;

    public int healthHealed;
    public int extraMeleeDamage = 1;
    public int extraRangedDamage = 1;
    
    public int extraMeleeDamagePerLevel = 1;
    public int extraRangedDamagePerLevel =1;



    private void Awake()
    {
        XpBar.SetMaxXP(maxExperience);
    }

    private void Start()
    {
        ResetTemporalStats();
    }

    public void gainExperience(int experienceGained)
    {
        actualExperience += experienceGained;
        if (actualExperience >= maxExperience) LevelUp();
        XpBar.SetXP(actualExperience);
    }


    public void LevelUp()
    {
        levelUPScreen.gameObject.SetActive(true);

        actualExperience = 0;
        maxExperience += extraExperiencePerLevel;
        level++;

    }

    //Level up efects

    public void Heal()
    {
        playerController.GetHealed(healthHealed);
    }

    public void MeleeDamage()
    {
        extraMeleeDamage += extraMeleeDamagePerLevel;
    }

    public void RangedDamage()
    {
        extraRangedDamage += extraRangedDamagePerLevel;
    }

    public void ResetTemporalStats()
    {
        extraMeleeDamage = 1; 
        extraRangedDamage = 1;
    }


}