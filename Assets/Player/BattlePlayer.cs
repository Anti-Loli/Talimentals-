using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : MonoBehaviour
{
    public Talimental Talimental;

    protected string name;
    protected int level;
    protected int maxHP;
    protected int attack;
    protected int defence;
    protected int speed;
    protected string element;
    protected string[] moves;
    protected string[] moveElements;

    
    private int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        name = Talimental.name;
        level = Talimental.level;
        maxHP = Talimental.health;
        attack = Talimental.attack;
        defence = Talimental.defence;
        speed = Talimental.speed;
        element = Talimental.element;
        assignArrays();
    }

   void assignArrays ()
    {

    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }
}
