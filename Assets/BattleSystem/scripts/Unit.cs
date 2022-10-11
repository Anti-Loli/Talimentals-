using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int level;

    public int damage;

    public int maxHP;
    public int currentHP;

    public bool TakeDamage(int dmg)
    {
        //deals damage
        currentHP -= dmg;

        //checks for death
        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    //heals character up to their max
    public void PlayerHeal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    
}