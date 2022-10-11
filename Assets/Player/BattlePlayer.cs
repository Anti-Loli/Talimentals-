using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : MonoBehaviour
{
    public Talimental playerTalimental;

    private string name;
    private int level;
    private int maxHP;
    private int attack;
    private int defence;
    private int speed;
    private string element;
    private string[] moves;
    private string[] moveElements;

    
    private int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        name = playerTalimental.name;
        level = playerTalimental.level;
        maxHP = playerTalimental.health;
        attack = playerTalimental.attack;
        defence = playerTalimental.defence;
        speed = playerTalimental.speed;
        element = playerTalimental.element;
        assignArrays();
    }

   void assignArrays ()
    {

    }
}
